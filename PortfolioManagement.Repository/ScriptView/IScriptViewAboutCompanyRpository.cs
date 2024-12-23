using PortfolioManagement.Entity.ScriptView;

namespace PortfolioManagement.Repository.ScriptView
{
    public interface IScriptViewAboutCompanyRpository
    {
        public Task<ScriptViewAboutCompanyEntity> SelectForAboutCompany(int id);
    }
}
