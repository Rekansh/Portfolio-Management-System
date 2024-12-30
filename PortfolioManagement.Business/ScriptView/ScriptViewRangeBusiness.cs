using AdvancedADO;
using Microsoft.Extensions.Configuration;
using PortfolioManagement.Entity.ScriptView;
using PortfolioManagement.Repository.ScriptView;
using System.Data;

namespace PortfolioManagement.Business.ScriptView
{
    public class ScriptViewRangeBusiness : CommonBusiness, IScriptViewRangeRepository
    {
        ISql sql;
        public ScriptViewRangeBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public async Task<ScriptViewRangeChartEntity> SelectForRange(int id)
        {
            sql.AddParameter("Id", id);
            ScriptViewRangeChartEntity scriptViewRangeChartEntity = await sql.ExecuteResultSetAsync<ScriptViewRangeChartEntity>("ScriptView_SelectForRange", CommandType.StoredProcedure, 2, mapData);

            FilterPrices(scriptViewRangeChartEntity);
            return scriptViewRangeChartEntity;
        }

        private void FilterPrices(ScriptViewRangeChartEntity scriptViewRangeChartEntity)
        {
            if (scriptViewRangeChartEntity.DayPrices != null)
            {
                var filteredPrices = scriptViewRangeChartEntity.DayPrices;
                int temp = 0;
                double previousVolume = 0;
                foreach (var price in filteredPrices)
                {

                    TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime istTime = TimeZoneInfo.ConvertTimeFromUtc(price.DateTime, indianTimeZone);
                    scriptViewRangeChartEntity.TimeSeries.Add(istTime.ToString("HH:mm"));

                    scriptViewRangeChartEntity.PriceSeriesData.Add(price.Price);

                    double volumeDifference = temp == 1 ? price.Volume : price.Volume - previousVolume;
                    scriptViewRangeChartEntity.VolumeSeriesData.Add(volumeDifference);
                    previousVolume = price.Volume;
                }
            }
        }

        private async Task mapData(int resultSet, ScriptViewRangeChartEntity scriptViewRangeChartEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    scriptViewRangeChartEntity.Script = await sql.MapDataAsync<ScriptViewRangeEntity>(reader);
                    break;
                case 1:
                    scriptViewRangeChartEntity.DayPrices.Add(await sql.MapDataAsync<ScriptViewRangeEntity>(reader));
                    break;
            }
        }
    }
}
