using PortfolioManagement.Entity.ScriptView;

namespace PortfolioManagement.Repository.ScriptView
{
    public interface IScriptViewCorporateActionRepository
    {
        public  Task<List<ScriptViewCorporateActionBonusEntity>> SelectForBonus(int id);
        public  Task<List<ScriptViewCorporateActionSplitEntity>> SelectForSplit(int id);
    }
}
