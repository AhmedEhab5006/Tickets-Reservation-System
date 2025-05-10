
using System.Net;
using System.Net.Mail;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.API.Helpers
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var senderEmail = "ticketnowdepi@gmail.com";
            var senderPassword = "gcuq nzdw xbqx rwuz";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };

            var Message = new MailMessage(
                from: senderEmail,
                to: email,
                subject: subject,
                body: message
            );

            return client.SendMailAsync(Message);

            
        }

        public int SendOtp(string email)
        {
            throw new NotImplementedException();
        }
    }
}
