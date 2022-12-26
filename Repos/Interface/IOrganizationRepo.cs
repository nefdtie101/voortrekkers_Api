using DataBaseModles;

namespace Repos.Interface;

public interface IOrganizationRepo
{
    public dynamic GetAllOrganizations();
    public bool CreateOrganization(Organization org);
    public bool EditOrganization(Organization org);
    public bool Delete(Organization org);
    public dynamic GetAllOrganizationById(string id);

}