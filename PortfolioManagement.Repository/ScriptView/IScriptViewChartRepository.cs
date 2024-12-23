using PortfolioManagement.Entity.ScriptView;

namespace PortfolioManagement.Repository.ScriptView
{
    public interface IScriptViewChartRepository
    {
        public  Task<ScriptViewChartEntity> SelectForChart(ScriptViewParameterEntity scriptViewParameterEntity);

    }
}
