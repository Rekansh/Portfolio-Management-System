using PortfolioManagement.Entity.Analysis;
using System.Data;

namespace PortfolioManagement.Repository.Analysis
{
    public interface IVolumeRepository
    {
        public  Task<VolumeGridEntity> SelectForVolume();
        public  Task MapVolumeEntity(int resultSet, VolumeGridEntity volumeGridEntity, IDataReader reader);
    }
}
