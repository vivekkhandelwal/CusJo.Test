using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MailScheduler.Entity
{
    public class EmailRecipient
    {
        [Key]
        public Guid RecipientId { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }
    }
}
