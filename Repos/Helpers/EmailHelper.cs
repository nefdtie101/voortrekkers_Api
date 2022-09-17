using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Repos.Interface;

namespace Repos.Helpers;

public class EmailHelper : IEmailHelper
{
    public bool sendResetPassword(string emaAddress, string ResetToken , string uri  )
    {
        var messiage = "<p>Druk <a href="+ uri + ResetToken +">hier</a> om wagwoord terug te stel";

                       var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("verify@nefdtco.co.za"));
        email.To.Add(MailboxAddress.Parse(emaAddress));
        email.Subject = "Leeuwenveld Voortrekkers herstel wagwoord";
        email.Body = new TextPart(TextFormat.Html) {Text = messiage};

        using var smtp = new SmtpClient();
        smtp.Connect("smtp.nefdtco.co.za", 587);
        smtp.Authenticate("verify@nefdtco.co.za","Eddie532411?");
        smtp.Send(email);
        smtp.Disconnect(true);

        return true;
    }
}