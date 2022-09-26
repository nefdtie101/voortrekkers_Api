using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Repos.Interface;

namespace Repos.Helpers;

public class EmailHelper : IEmailHelper
{
    private readonly IConfiguration _config;
    public EmailHelper(IConfiguration config)
    {
        _config = config;
    }
    public bool sendResetPassword(string emaAddress, string ResetToken , string uri  )
    {
        var messiage = "<p>Druk <a href="+ uri + "/ResetPassword?Key=" + ResetToken +">hier</a> om wagwoord terug te stel";

                       var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.GetValue<string>("Email:Username")));
        email.To.Add(MailboxAddress.Parse(emaAddress));
        email.Subject = "Leeuwenveld Voortrekkers herstel wagwoord";
        email.Body = new TextPart(TextFormat.Html) {Text = messiage};

        using var smtp = new SmtpClient();
        smtp.Connect(_config.GetValue<string>("Email:uri"), Int32.Parse(_config.GetValue<string>("Email:Port")));
        smtp.Authenticate(_config.GetValue<string>("Email:Username"),_config.GetValue<string>("Email:Password"));
        smtp.Send(email);
        smtp.Disconnect(true);

        return true;
    }
}