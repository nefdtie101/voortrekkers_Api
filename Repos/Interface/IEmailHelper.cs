namespace Repos.Interface;

public interface IEmailHelper
{
    public bool sendResetPassword(string emaAddress, string ResetToken, string uri);
    public bool VerifyUser(string emaAddress, string ResetToken, string uri);
    public bool InskrywingOntvang(string emailAddress, string eventName);
    public bool MarkAsPaid(string emailAddress, string eventName);
    public bool SendBulkEmail(string subject, string Message, bool Staatmaker);
}