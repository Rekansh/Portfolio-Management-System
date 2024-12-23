using PortfolioManagement.Entity.Master;
using System.Data;

namespace PortfolioManagement.Repository.Master
{
    public interface IScriptRepository
    {
        public ScriptEntity MapData(IDataReader reader);
        public  Task<ScriptEntity> SelectForRecord(int id);
        public  Task<List<ScriptMainEntity>> SelectForLOV(ScriptParameterEntity scriptParameterEntity);
        public  Task<ScriptGridEntity> SelectForGrid(ScriptParameterEntity scriptParameterEntity);
        public  Task MapGridEntity(int resultSet, ScriptGridEntity scriptGridEntity, IDataReader reader);
        public  Task<int> Insert(ScriptEntity scriptEntity);
        public  Task<int> Update(ScriptEntity scriptEntity);
        public  Task Delete(int id);
        public  Task<List<ScriptEntity>> SelectForScrap();

    }
}
