using DataBaseModles;
using DataBaseModles.ViewModels;

namespace Repos.Interface;

public interface IUserRepo
{
    public dynamic GetGetUsers();

    public bool CreateUser(UserView user);
}