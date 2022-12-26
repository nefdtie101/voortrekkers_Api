using DataBaseModles;

namespace Repos.Interface;

public interface IEmailRepo
{
    public bool AddToEmailList(EmailModel email);
    public dynamic GetAllEmailAddresses(bool isStaatmaker);
    public bool MarkAsStaatmker(string email);
    public bool UnSubscribeEmail(string Email);
}