using PortfolioManagement.Entity.Index;
using System.Data;

namespace PortfolioManagement.Repository.Index
{
    public interface IHeaderRepository
    {
        public  Task<HeaderGridEntity> SelectForGrid();
        public  Task MapGridEntity(int resultSet, HeaderGridEntity headerGridEntity, IDataReader reader);
        public Task<HeaderEntity> SelectForIndex();
    }
}
