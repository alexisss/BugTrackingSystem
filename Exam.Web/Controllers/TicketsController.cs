using Exam.Models;
using Exam.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Exam.Web.Controllers
{
    public class TicketsController : BaseController
    {
        //
        // GET: /Tickets/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var ticket = this.Data.Tickets.All()
                .Where(t => t.Id == id)
                .Select(DetailedTicketViewModel.FromTicket)
                .FirstOrDefault();

            return View(ticket);
        }

        [Authorize]
        public ActionResult Add()
        {
            ViewBag.CategoryId = new SelectList(this.Data.Categories.All(), "Id", "Name");
            ViewBag.PriorityId = new SelectList(Enum.GetNames(typeof(Priority)));
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                this.Data.Tickets.Add(ticket);
                var userId = this.User.Identity.GetUserId();
                var user = this.Data.Users.All().Where(u => u.Id == userId).FirstOrDefault();
                ticket.Author = user;
                ticket.Priority = ticket.Priority;
                user.Points++;
                this.Data.SaveChanges();
                return RedirectToAction("ViewAllTickets");
            }

            ViewBag.CategoryId = new SelectList(this.Data.Categories.All(), "Id", "Name", ticket.CategoryId);
            ViewBag.PriorityId = new SelectList(Enum.GetNames(typeof(Priority)));
            return View(ticket);
        }

        [Authorize]
        public ActionResult ViewAllTickets()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetAllTickets([DataSourceRequest] DataSourceRequest request)
        {
            var ticket = this.Data.Tickets.All().Select(TicketViewModel.FromTicket).ToList();

            return Json(ticket.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitViewModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var username = this.User.Identity.GetUserName();
                var userId = this.User.Identity.GetUserId();

                this.Data.Comments.Add(new Comment()
                {
                    AuthorId = userId,
                    Content = commentModel.Description,
                    TicketId = commentModel.TicketId,
                });

                this.Data.SaveChanges();

                var viewModel = new CommentsViewModel { Author = username, Content = commentModel.Description };
                return PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }

        public JsonResult GetCategories()
        {
            var result = this.Data.Categories
                .All()
                .Select(x => new
                {
                    Name = x.Name
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Search(SubmitSearchModel submitModel)
        {
            var result = this.Data.Tickets.All();

            if (submitModel.CategorySearch != "Select Category")
            {
                result = result.Where(x => x.Category.Name == submitModel.CategorySearch);
            }

            var endResult = result.Select(x => new TicketViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author.UserName,
                Category = x.Category.Name,
                Priority = x.Priority,
                CommentsCount = x.Comments.Count()
            });

            return View(endResult);
        }
    }
}