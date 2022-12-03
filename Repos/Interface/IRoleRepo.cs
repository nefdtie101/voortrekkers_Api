using DataBaseModles;

namespace Repos.Interface;

public interface IRoleRepo
{
    public dynamic GetAllRoles();
    public bool CreateRole(RoleModel role);
    public bool EditRole(RoleModel role);
    public bool Delete(RoleModel role);
    
}