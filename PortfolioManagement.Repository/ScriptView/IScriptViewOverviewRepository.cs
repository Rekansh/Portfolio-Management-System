using PortfolioManagement.Entity.ScriptView;

namespace PortfolioManagement.Repository.ScriptView
{
    public interface IScriptViewOverviewRepository
    {
        public Task<ScriptViewOverviewEntity> SelectForOverview(int id);

    }
}
