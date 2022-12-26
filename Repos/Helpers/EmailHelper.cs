using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Repos.Interface;

namespace Repos.Helpers;

public class EmailHelper : IEmailHelper
{
    private readonly IConfiguration _config;
    private IEmailRepo _emailRepo;
    public EmailHelper(IConfiguration config , IEmailRepo repo)
    {
        _config = config;
        _emailRepo = repo;
    }
    public bool sendResetPassword(string emaAddress, string ResetToken , string uri  )
    {
        var messiage = "<p>Druk <a href="+ uri + "/ResetPassword?Key=" + ResetToken +">hier</a> om wagwoord terug te stel";

                       var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.GetValue<string>("Email:Username")));
        email.To.Add(MailboxAddress.Parse(emaAddress));
        email.Subject = "Leeuwenveld Staatmakers herstel wagwoord";
        email.Body = new TextPart(TextFormat.Html) {Text = messiage};

        using var smtp = new SmtpClient();
        smtp.Connect(_config.GetValue<string>("Email:uri"), Int32.Parse(_config.GetValue<string>("Email:Port")));
        smtp.Authenticate(_config.GetValue<string>("Email:Username"),_config.GetValue<string>("Email:Password"));
        smtp.Send(email);
        smtp.Disconnect(true);

        return true;
    }
    
    public bool VerifyUser(string emaAddress, string ResetToken , string uri  )
    {
        var messiage = "<p>Druk <a href="+ uri + "/ResetPassword?Key=" + ResetToken +">hier</a> om wagwoord te stel en e-pos te verifieer.";

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.GetValue<string>("Email:Username")));
        email.To.Add(MailboxAddress.Parse(emaAddress));
        email.Subject = "Leeuwenveld Staatmakers Welkom";
        email.Body = new TextPart(TextFormat.Html) {Text = messiage};

        using var smtp = new SmtpClient();
        smtp.Connect(_config.GetValue<string>("Email:uri"), Int32.Parse(_config.GetValue<string>("Email:Port")));
        smtp.Authenticate(_config.GetValue<string>("Email:Username"),_config.GetValue<string>("Email:Password"));
        smtp.Send(email);
        smtp.Disconnect(true);

        return true;
    }

    public bool InskrywingOntvang(string emailAddress , string eventName )
    {
        try
        {
            var messiage = "Baie dankie ons het jou inskrywing ontvang";  
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetValue<string>("Email:Username")));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = eventName;
            email.Body = new TextPart(TextFormat.Text) {Text = messiage};
        
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetValue<string>("Email:uri"), Int32.Parse(_config.GetValue<string>("Email:Port")));
            smtp.Authenticate(_config.GetValue<string>("Email:Username"),_config.GetValue<string>("Email:Password"));
            smtp.Send(email);
            smtp.Disconnect(true);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public bool MarkAsPaid(string emailAddress , string eventName )
    {
        try
        {
            var messiage = "Baie dankie ons het jou betaling ontvang ";  
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetValue<string>("Email:Username")));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = eventName;
            email.Body = new TextPart(TextFormat.Text) {Text = messiage};
        
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetValue<string>("Email:uri"), Int32.Parse(_config.GetValue<string>("Email:Port")));
            smtp.Authenticate(_config.GetValue<string>("Email:Username"),_config.GetValue<string>("Email:Password"));
            smtp.Send(email);
            smtp.Disconnect(true);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    
    public bool SendBulkEmail(string subject, string Message , bool Staatmaker)
    {
        try
        {
            var emailAddress = _emailRepo.GetAllEmailAddresses(Staatmaker);
            foreach (var emails in emailAddress)
            {
                var messiage = "<p>"+Message+"</p>" + "<Footer>" +
                               "<p><a href='"+ _config.GetValue<string>("DeployUriFrontend") + "/UnSebscribe?email="+emails.Email+"'"
                               +"</a>Unsubscribe</p>" +
                               "</Footer>";
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetValue<string>("Email:Username")));
                email.To.Add(MailboxAddress.Parse(emails.Email));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) {Text = messiage};
        
                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetValue<string>("Email:uri"), Int32.Parse(_config.GetValue<string>("Email:Port")));
                smtp.Authenticate(_config.GetValue<string>("Email:Username"),_config.GetValue<string>("Email:Password"));
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}