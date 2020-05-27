using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;

namespace MailScheduler.Entity
{
    public class EmailTrack
    {
        [Key]
        public Guid TrackId { get; set; }

        public String ReferenceCode { get; set; }

        public Guid RecipientId { get; set; }

        public bool IsLinkOpened { get; set; }

        public bool IsThankYouMailSent { get; set; }

        public DateTime? LastRemindedDate { get; set; }

        public int ReminderCount { get; set; }
    }
}
