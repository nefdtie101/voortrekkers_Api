using DataBaseModles;

namespace Repos.Interface;

public interface ILoginRepo
{
    public dynamic Login(LoginModel loginModel);
    public bool resetPassword();
}