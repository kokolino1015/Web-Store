using WebStore.Data.Entities;

namespace WebStore.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
