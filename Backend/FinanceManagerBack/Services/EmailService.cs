using FinanceManagerBack.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FinanceManagerBack.Services
{
    public class EmailService : IMessageEmailService
    {
        public async Task SendMessage(string email, string subject, string message)
        {
            MailMessage message2 = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message2.From = new MailAddress("sukhyiroman@gmail.com");
            message2.To.Add(new MailAddress(email));
            message2.Subject = "Test";
            message2.IsBodyHtml = true; //to make message body as html
            message2.Body = message;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sukhyiroma@gmail.com", "pass-1234");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.Send(message2);
        }
    }
}