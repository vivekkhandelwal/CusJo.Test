using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MailScheduler.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Add(String name, String email)
        {
            SchedulerContext db = new SchedulerContext();

            db.EmailRecipients.Add(new Entity.EmailRecipient()
            {
                RecipientId = Guid.NewGuid(),
                Name = name,
                Email = email
            });

            db.SaveChanges();

            return Content("Added successfully");
        }

        public IActionResult Link(String id)
        {
            SchedulerContext db = new SchedulerContext();

            var track = (from a in db.EmailTracks where a.ReferenceCode == id select a).FirstOrDefault();

            if (track == null)
                return NotFound();

            track.IsLinkOpened = true;

            db.SaveChanges();

            return View();
        }
    }
}
