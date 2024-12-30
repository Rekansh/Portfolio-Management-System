using AdvancedADO;
using Microsoft.Extensions.Configuration;
using PortfolioManagement.Entity.ScriptView;
using PortfolioManagement.Repository.ScriptView;
using System.Data;

namespace PortfolioManagement.Business.ScriptView
{
    public class ScriptViewCorporateActionBusiness : CommonBusiness, IScriptViewCorporateActionRepository
    {
        ISql sql;
        public ScriptViewCorporateActionBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();

        }
        public async Task<List<ScriptViewCorporateActionBonusEntity>> SelectForBonus(int id)
        {
            sql.AddParameter("Id", id);
            return await sql.ExecuteListAsync<ScriptViewCorporateActionBonusEntity>("ScriptView_CorporateAction_SelectForBonus", CommandType.StoredProcedure);
        }
        public async Task<List<ScriptViewCorporateActionSplitEntity>> SelectForSplit(int id)
        {
            sql.AddParameter("Id", id);
            return await sql.ExecuteListAsync<ScriptViewCorporateActionSplitEntity>("ScriptView_CorporateAction_SelectForSplit", CommandType.StoredProcedure);
        }
    }
}
