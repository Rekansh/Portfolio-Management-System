using CommonLibrary.SqlDB;
using CommonLibrary;
using Microsoft.Extensions.Configuration;
using PortfolioManagement.Entity.ScriptView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioManagement.Repository.ScriptView;

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
            ScriptViewRangeChartEntity scriptViewRangeChartEntity = new ScriptViewRangeChartEntity();
            sql.AddParameter("Id", id);
            await sql.ExecuteEnumerableMultipleAsync<ScriptViewRangeChartEntity>("ScriptView_SelectForRange", CommandType.StoredProcedure, 2, scriptViewRangeChartEntity, mapData);

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
                    scriptViewRangeChartEntity.Script = await sql.MapDataDynamicallyAsync<ScriptViewRangeEntity>(reader);
                    break;
                case 1:
                    scriptViewRangeChartEntity.DayPrices.Add(await sql.MapDataDynamicallyAsync<ScriptViewRangeEntity>(reader));
                    break;
            }
        }
    }
}
