using PortfolioManagement.Entity.Index;

namespace PortfolioManagement.Repository.Index
{
    public interface IIndexChartRepository
    {
        public Task<IndexChartGridEntity> SelectForIndexChart(IndexChartParameterEntity indexChartParameterEntity);
    }
}
