using System.Net;
using System.Net.Mail;

namespace Api.Emailer {
    public static class EmailService {
        public static void SendEmail(string toEmail, string subject, string body) {
            var fromAddress = new MailAddress("kmarmet1@gmail.com", "Me");
            var toAddress = new MailAddress("kmarmet1@gmail.com", "Peaceful coParenting Support");
            const string fromPassword = "Kramer12$";


            var smtp = new SmtpClient {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            smtp.Send(message);
        }
    }
}