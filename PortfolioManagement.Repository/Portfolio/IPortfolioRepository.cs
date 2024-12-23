using PortfolioManagement.Entity.Transaction;
using PortfolioManagement.Entity.Transaction.StockTransaction;

namespace PortfolioManagement.Repository.Portfolio
{
    public interface IPortfolioRepository
    {
        public Task<List<ProtfolioEntity>> Select(ProtfolioParameterEntity protfolioParameterEntity);
        public Task<PortfolioReportEntity> SelectPortfolioReport(StockTransactionParameterEntity transactionParameterEntity);

    }
}
