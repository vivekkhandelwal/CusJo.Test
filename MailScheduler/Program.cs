using System;

namespace MailScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            // Schedule this job using cron expression or windows Task Scheduler

            // To run the code need to update below steps
            
            // 1. Update the connection string in
            /// <see cref="MailScheduler.Entity.SchedulerContext"/>

            // 2. Execute "Update-Database" in Package Manager Console.
            
            // 3. Update the mail credentials in below file
            /// <see cref="MailScheduler.Mail.MailClient"/>

            Scheduler.Instance.RunAsync().GetAwaiter().GetResult();
        }
    }
}
