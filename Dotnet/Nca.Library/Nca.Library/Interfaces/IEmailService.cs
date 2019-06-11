using System.Threading.Tasks;

namespace Nca.Library.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
