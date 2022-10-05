namespace Repos.Interface;

public interface IEmailHelper
{
    public bool sendResetPassword(string emaAddress, string ResetToken, string uri);
    public bool VerifyUser(string emaAddress, string ResetToken, string uri);
}