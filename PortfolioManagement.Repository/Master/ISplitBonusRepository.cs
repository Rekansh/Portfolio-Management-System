using PortfolioManagement.Entity.Master;
using System.Data;

namespace PortfolioManagement.Repository.Master
{
    public interface ISplitBonusRepository
    {
        public SplitBonusEntity MapData(IDataReader reader);
        public  Task<SplitBonusEntity> SelectForRecord(int id);
        public  Task<SplitBonusGridEntity> SelectForGrid();
        public  Task MapGridEntity(int resultSet, SplitBonusGridEntity splitBonusGridEntity, IDataReader reader);
        public  Task<int> Insert(SplitBonusEntity splitBonusEntity);
        public  Task<int> Update(SplitBonusEntity splitBonusEntity);
        public  Task Delete(int id);
        public  Task<int> SplitBonusApply(int id, bool IsApply);
    }
}
