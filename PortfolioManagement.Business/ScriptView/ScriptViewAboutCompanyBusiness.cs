using AdvancedADO;
using Microsoft.Extensions.Configuration;
using PortfolioManagement.Entity.ScriptView;
using PortfolioManagement.Repository.ScriptView;
using System.Data;

namespace PortfolioManagement.Business.ScriptView
{
    public class ScriptViewAboutCompanyBusiness:CommonBusiness, IScriptViewAboutCompanyRpository
    {
        ISql sql;
        public ScriptViewAboutCompanyBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();

        }
        public async Task<ScriptViewAboutCompanyEntity> SelectForAboutCompany(int id)
        {
            sql.AddParameter("Id", id);
            return await sql.ExecuteRecordAsync<ScriptViewAboutCompanyEntity>("ScriptView_SelectForAboutCompany", CommandType.StoredProcedure);
        }
    }
}
