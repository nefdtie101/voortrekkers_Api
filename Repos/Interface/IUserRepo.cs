using DataBaseModles;

namespace Repos.Interface;

public interface IUserRepo
{
    bool CreateNewUser(UserModel user);
}