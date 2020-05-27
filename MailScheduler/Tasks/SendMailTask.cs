using MailScheduler.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using MailScheduler.Mail;

namespace MailScheduler.Tasks
{
    public class SendMailTask : ITask
    {
        private static Random random = new Random();

        public async Task RunAsync()
        {
            SchedulerContext db = new SchedulerContext();

            var emails = (from a in db.EmailRecipients where !db.EmailTracks.Any(t => t.RecipientId == a.RecipientId) select a).ToList();

            foreach (var item in emails)
            {
                await SendMail(db, item);
            }
        }

        public async Task SendMail(SchedulerContext db, EmailRecipient item)
        {
            String referenceCode = await SendEmail(item);

            await UpdateTrack(db, item, referenceCode);
        }

        private async Task<String> SendEmail(EmailRecipient item)
        {
            StringBuilder contructMail = new StringBuilder();

            var referenceCode = GenerateReferenceCode();

            contructMail.AppendLine("<p>Hi, </p>");
            contructMail.AppendLine("<p></p>");
            contructMail.AppendLine("<p>Please click below link.</p>");

            String link = "https://localhost:44303/Home/Link/" + referenceCode;

            contructMail.AppendLine($"<p><a href=\"{link}\">{link}</a></p>");

            await MailClient.SendMailAsync(item.Email, item.Name, "Welcome mail", contructMail.ToString());

            return referenceCode;
        }

        public async Task UpdateTrack(SchedulerContext db, EmailRecipient item, String referenceCode)
        {
            db.EmailTracks.Add(new EmailTrack()
            {
                TrackId = Guid.NewGuid(),
                RecipientId = item.RecipientId,
                ReferenceCode = referenceCode,
                IsLinkOpened = false,
                IsThankYouMailSent = false,
                LastRemindedDate = DateTime.Today
            });

            await db.SaveChangesAsync();
        }

        private string GenerateReferenceCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 64)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
