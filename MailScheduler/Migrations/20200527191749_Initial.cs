using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailScheduler.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailRecipients",
                columns: table => new
                {
                    RecipientId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailRecipients", x => x.RecipientId);
                });

            migrationBuilder.CreateTable(
                name: "EmailTracks",
                columns: table => new
                {
                    TrackId = table.Column<Guid>(nullable: false),
                    ReferenceCode = table.Column<string>(nullable: true),
                    RecipientId = table.Column<Guid>(nullable: false),
                    IsLinkOpened = table.Column<bool>(nullable: false),
                    IsThankYouMailSent = table.Column<bool>(nullable: false),
                    LastRemindedDate = table.Column<DateTime>(nullable: true),
                    ReminderCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTracks", x => x.TrackId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailRecipients");

            migrationBuilder.DropTable(
                name: "EmailTracks");
        }
    }
}
