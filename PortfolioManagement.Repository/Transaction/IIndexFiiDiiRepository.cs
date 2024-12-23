using PortfolioManagement.Entity.Transaction;

namespace PortfolioManagement.Repository.Transaction
{
    public interface IIndexFiiDiiRepository
    {
        public Task<IndexFiiDiiChartEntity> SelectForChart(IndexFiiDiiParameterEntity indexFiiDiiParameterEntity);

    }
}
