using PortfolioManagement.Entity.ScriptView;

namespace PortfolioManagement.Repository.ScriptView
{
    public interface IScriptViewRangeRepository
    {
        public  Task<ScriptViewRangeChartEntity> SelectForRange(int id);
    }
}
