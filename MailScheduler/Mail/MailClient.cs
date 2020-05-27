using MailKit.Security;
using MailScheduler.Tasks;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailScheduler.Mail
{
    public class MailClient
    {
        public static async Task SendMailAsync(String toEmail, String toName, String subject, String bodyText)
        {
            var from = new MailboxAddress("Vivek Khandelwal", "vivek.b.khandelwal@gmail.com");

            var messageToSend = new MimeMessage
            {
                Sender = from,
                Subject = subject,
            };

            messageToSend.From.Add(from);
            
            if(String.IsNullOrWhiteSpace(toName))
                messageToSend.To.Add(new MailboxAddress(toEmail));
            else
                messageToSend.To.Add(new MailboxAddress(toName, toEmail));

            messageToSend.Body = new TextPart(TextFormat.Html) { Text = bodyText };

            String fromUsername = from.Address; 
            string fromPassword = "";               // To be updated

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(fromUsername, fromPassword);
                await smtp.SendAsync(messageToSend);
                await smtp.DisconnectAsync(true);
            }        
        }
    }
}
