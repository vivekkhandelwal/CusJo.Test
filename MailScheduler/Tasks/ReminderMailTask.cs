using MailScheduler.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using MailScheduler.Mail;

namespace MailScheduler.Tasks
{
    public class ReminderMailTask : ITask
    {
        public async Task RunAsync()
        {
            SchedulerContext db = new SchedulerContext();

            var tracks = (from a in db.EmailTracks where !a.IsLinkOpened || !a.IsThankYouMailSent select a).ToList();

            var emails = (from a in db.EmailRecipients select a).ToDictionary(a => a.RecipientId);

            foreach (var item in tracks)
            {
                if (item.IsThankYouMailSent)
                    continue;

                if (item.IsLinkOpened && !item.IsThankYouMailSent)
                    await SendThankYouMail(db, emails[item.RecipientId], item);

                else if (!item.IsLinkOpened && item.LastRemindedDate != DateTime.Today && item.ReminderCount < 3)
                    await SendReminderMail(db, emails[item.RecipientId], item);
            }
        }

        public async Task SendReminderMail(SchedulerContext db, EmailRecipient item, EmailTrack track)
        {
            await SendReminderEmail(item, track);

            track.LastRemindedDate = DateTime.Today;
            track.ReminderCount++;

            await db.SaveChangesAsync();
        }

        private async Task SendReminderEmail(EmailRecipient item, EmailTrack track)
        {
            StringBuilder contructMail = new StringBuilder();

            contructMail.AppendLine("<p>Hi, </p>");
            contructMail.AppendLine("<p></p>");
            contructMail.AppendLine("<p>In case you might have missed our previous mail.</p>");
            contructMail.AppendLine("<p>Please click below link.</p>");

            String link = "https://localhost:44303/Home/Link/" + track.ReferenceCode;

            contructMail.AppendLine($"<p><a href=\"{link}\">{link}</a></p>");

            await MailClient.SendMailAsync(item.Email, item.Name, "Welcome mail", contructMail.ToString());
        }


        public async Task SendThankYouMail(SchedulerContext db, EmailRecipient item, EmailTrack track)
        {
            await SendThankYouMail(item, track);

            track.IsThankYouMailSent = true;

            await db.SaveChangesAsync();
        }

        private async Task SendThankYouMail(EmailRecipient item, EmailTrack track)
        {
            StringBuilder contructMail = new StringBuilder();

            contructMail.AppendLine("<p>Hi, </p>");
            contructMail.AppendLine("<p></p>");
            contructMail.AppendLine("<p>Thank you for clicking the link in our previous mail.</p>");

            await MailClient.SendMailAsync(item.Email, item.Name, "Welcome mail", contructMail.ToString());
        }

    }
}
