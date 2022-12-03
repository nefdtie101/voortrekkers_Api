using DataBaseModles;

namespace Repos.Interface;


public interface IModuleRepo
{
    public dynamic GetAllModules();
    public bool CreateModule(ModuleModel module);
    public bool EditModule(ModuleModel module);
    public bool Delete(ModuleModel module);

}