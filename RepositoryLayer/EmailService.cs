using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer
{
    public class EmailService
    {
        public static void SendEmail(string email, string link)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("dstudent759@gmail.com", "dshyyrhiniqlpsxy");

                MailMessage msg = new MailMessage();
                msg.To.Add(email);
                msg.From = new MailAddress("dstudent759@gmail.com");
                msg.Subject = "Password Reset Link BookStore Application";
                msg.Body = $"https://localhost:44303/swagger/index.html{link}";
                client.Send(msg);
            }
        }
    }
}
