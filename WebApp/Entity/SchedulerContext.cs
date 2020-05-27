using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using WebApp.Entity;

namespace MailScheduler.Entity
{
    public class SchedulerContext : DbContext
    {
        public SchedulerContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public SchedulerContext()
        {
        }

        public DbSet<EmailRecipient> EmailRecipients { get; set; }

        public DbSet<EmailTrack> EmailTracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=SchedulerDb;");
        }
    }
}
