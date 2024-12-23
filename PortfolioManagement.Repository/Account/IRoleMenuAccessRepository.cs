using PortfolioManagement.Entity.Account;

namespace PortfolioManagement.Repository.Account
{
    public interface IRoleMenuAccessRepository
    {
        public List<RoleMenuAccessEntity> SelectList();
        public  Task<List<RoleMenuAccessEntity>> SelectListByRoleIdParentId(RoleMenuAccessEntity roleMenuAccessEntity);
        public  Task<int> Bulk(RoleEntity roleEntity);
    }
}
