﻿using AdvancedADO;
using CommonLibrary;
using Microsoft.Extensions.Configuration;
using PortfolioManagement.Entity.Master;
using PortfolioManagement.Entity.ScriptView;
using PortfolioManagement.Repository.ScriptView;
using System.Data;

namespace PortfolioManagement.Business.ScriptView
{
    public class ScriptViewChartBusiness : CommonBusiness, IScriptViewChartRepository
    {
        ISql sql;

        public ScriptViewChartBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public ScriptMainEntity MapScriptMain(IDataReader reader)
        {
            ScriptMainEntity scriptMain = new ScriptMainEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        scriptMain.Id = MyConvert.ToByte(reader["Id"]);
                        break;
                    case "Name":
                        scriptMain.Name = MyConvert.ToString(reader["Name"]);
                        break;
                }
            }
            return scriptMain;
        }

        public async Task<ScriptViewChartEntity> SelectForChart(ScriptViewParameterEntity scriptViewParameterEntity)
        {
            sql.AddParameter("ScriptId", scriptViewParameterEntity.ScriptId);
            sql.AddParameter("FromDate", DbType.DateTime, ParameterDirection.Input , scriptViewParameterEntity.FromDate);
            sql.AddParameter("ToDate",DbType.DateTime, ParameterDirection.Input, scriptViewParameterEntity.ToDate);

            ScriptViewChartEntity scriptViewChartEntity = await sql.ExecuteResultSetAsync<ScriptViewChartEntity>("ScriptView_SelectForChart", CommandType.StoredProcedure , 2, mapData);

            FilterPrices(scriptViewChartEntity, scriptViewParameterEntity);
            return scriptViewChartEntity;
        }

        private void FilterPrices(ScriptViewChartEntity scriptViewChartEntity, ScriptViewParameterEntity scriptViewParameterEntity)
        {
            if (scriptViewChartEntity.Prices != null)
            {
                var filteredPrices = scriptViewChartEntity.Prices;
                foreach (var price in filteredPrices)
                {
                    //scriptViewChartEntity.Dates.Add(price.Date.ToString("yyyy-MM-dd HH:mm:ss+0000"));
                    scriptViewChartEntity.Dates.Add(price.Date.ToString("yyyy-MM-dd"));

                    scriptViewChartEntity.CandelSeriesData.Add(new object[] {price.Date.ToString("yyyy-MM-dd HH:mm:ss+0000"), price.Open, price.Close, price.High, price.Low });
                    scriptViewChartEntity.PriceSeriesData.Add(price.Price);
                    scriptViewChartEntity.VolumeSeriesData.Add(price.Volume);

                }
            }
        }

        private async Task mapData(int resultSet, ScriptViewChartEntity scriptViewChartEntity, IDataReader reader)
        {
            switch (resultSet)
            {

                case 0:
                    scriptViewChartEntity.Script = MapScriptMain(reader);
                    break;
                case 1:
                    scriptViewChartEntity.Prices.Add(await sql.MapDataAsync<ScriptViewPriceEntity>(reader));
                    break;
            }
        }
    }
}
    