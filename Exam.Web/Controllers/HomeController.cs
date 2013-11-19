using Exam.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (this.HttpContext.Cache["HomePageTickets"] == null)
            {
                var listOfTickets = this.Data.Tickets.All()
                    .OrderByDescending(x => x.Comments.Count())
                    .Take(6)
                    .Select(x => new TicketViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Author = x.Author.UserName,
                        Category = x.Category.Name,
                        CommentsCount = x.Comments.Count()
                    }).ToList();

                this.HttpContext.Cache.Add("HomePageTickets", listOfTickets.ToList(), null, DateTime.Now.AddHours(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Default, null);
            }

            return View(this.HttpContext.Cache["HomePageTickets"]);
        }

    }
}