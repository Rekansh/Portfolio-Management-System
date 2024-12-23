using PortfolioManagement.Entity.Transaction;

namespace PortfolioManagement.Repository.Portfolio
{
    public interface IPortfolioDatewiseReository
    {
        public Task ProcessPortfolioDatewise(DateTime current, DateTime portfolioDateTime);
        public Task<List<PortfolioDatewiseReportEntity>> SelectForPortfolioDatewiseData(PortfolioDatewiseParameterEntity portfolioDatewiseParameterEntity);
    }
}
