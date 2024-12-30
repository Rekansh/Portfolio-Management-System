using AdvancedADO;
using Microsoft.Extensions.Configuration;
using PortfolioManagement.Entity.Analysis;
using PortfolioManagement.Repository.Analysis;
using System.Data;

namespace PortfolioManagement.Business.Analysis
{
    public class VolumeBusiness : CommonBusiness, IVolumeRepository
    {
        ISql sql;
        public VolumeBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }
        public async Task<VolumeGridEntity> SelectForVolume()
        {
            return await sql.ExecuteResultSetAsync<VolumeGridEntity>("Analysis_SelectForVolume", CommandType.StoredProcedure, 1, MapVolumeEntity);
        }
        public async Task MapVolumeEntity(int resultSet, VolumeGridEntity volumeGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    volumeGridEntity.Volumes.Add(await sql.MapDataAsync<VolumeEntity>(reader));
                    break;
            }
        }

    }
}
