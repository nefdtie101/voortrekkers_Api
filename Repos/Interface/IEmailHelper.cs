namespace Repos.Interface;

public interface IEmailHelper
{
    public bool sendResetPassword(string emaAddress, string ResetToken, string uri);
}