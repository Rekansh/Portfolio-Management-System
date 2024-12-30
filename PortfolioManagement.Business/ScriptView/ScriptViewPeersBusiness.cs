using AdvancedADO;
using Microsoft.Extensions.Configuration;
using PortfolioManagement.Entity.ScriptView;
using PortfolioManagement.Repository.ScriptView;
using System.Data;

namespace PortfolioManagement.Business.ScriptView
{
    public class ScriptViewPeersBusiness :CommonBusiness, IScriptViewPeersRepository
    {
        ISql sql;
        public ScriptViewPeersBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public async Task<List<ScriptViewPeersEntity>> SelectForPeers(int id)
        {
            sql.AddParameter("Id", id);
            return await sql.ExecuteListAsync<ScriptViewPeersEntity>("ScriptView_SelectForPeers", CommandType.StoredProcedure);
        }
    }
}
