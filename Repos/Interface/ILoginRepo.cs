using DataBaseModles;
using Microsoft.AspNetCore.Http;
using Voortrekkers.Pages.ResetPassword;

namespace Repos.Interface;

public interface ILoginRepo
{
    public dynamic Login(LoginModel loginModel);
    public bool resetPassword(LoginModel loginModel);
    public bool CreatePassword(ResetPasswordModel resetPassword, HttpContext http);

    public JwtModel refreshJwtToken(string token, string email);
}