using System.Threading.Tasks;

namespace FinanceManagerBack.Interfaces
{
    public interface IMessageEmailService
    {
        Task SendMessage(string email, string subject, string message);
    }
}