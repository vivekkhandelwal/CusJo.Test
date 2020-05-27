using MailScheduler.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailScheduler
{
    public class Scheduler
    {
        public static Scheduler Instance { get; } = new Scheduler();

        public Scheduler()
        {

        }

        public async System.Threading.Tasks.Task RunAsync()
        {
            await new SendMailTask().RunAsync();

            await new ReminderMailTask().RunAsync();
        }
    }
}
