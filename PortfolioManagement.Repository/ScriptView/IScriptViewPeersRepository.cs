using PortfolioManagement.Entity.ScriptView;

namespace PortfolioManagement.Repository.ScriptView
{
    public interface IScriptViewPeersRepository
    {
        public Task<List<ScriptViewPeersEntity>> SelectForPeers(int id);

    }
}
