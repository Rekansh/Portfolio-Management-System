using AdvancedADO;
using Microsoft.Extensions.Configuration;
using PortfolioManagement.Entity.Index;
using PortfolioManagement.Repository.Index;
using System.Data;

namespace PortfolioManagement.Business.Index
{
    public class HeaderBusiness : CommonBusiness,IHeaderRepository
    {
        ISql sql;
        public HeaderBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }
        public async Task<HeaderGridEntity> SelectForGrid()
        {
            return await sql.ExecuteResultSetAsync<HeaderGridEntity>("Index_SelectForNifty50", CommandType.StoredProcedure, 1, MapGridEntity);
        }
        public async Task MapGridEntity(int resultSet, HeaderGridEntity headerGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    headerGridEntity.Nifty50.Add(await sql.MapDataAsync<HeaderNifty50Entity>(reader));
                    break;
            }
        }
        public async Task<HeaderEntity> SelectForIndex()
        {
            return await sql.ExecuteRecordAsync<HeaderEntity>("Index_SelectForHeader", CommandType.StoredProcedure);
        }
    }
}
