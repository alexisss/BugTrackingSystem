using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Exam.Web.Models
{
    public class DetailedTicketViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }
       
        public string Category { get; set; }

        [NotContainBug(ErrorMessage = "The word bug is unacceptable")]
        public string Title { get; set; }
              
        public Priority Priority { get; set; }

        public string ScreenShotUrl { get; set; }

        public string Description { get; set; }

        public IEnumerable<CommentsViewModel> Comments { get; set; }

         public static Expression<Func<Ticket, DetailedTicketViewModel>> FromTicket
        {
            get
            {
                return ticket => new DetailedTicketViewModel
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    Author = ticket.Author.UserName,
                    Category = ticket.Category.Name,
                    Description = ticket.Description,
                    Priority = ticket.Priority,
                    ScreenShotUrl = ticket.ScreenShotUrl,
                    Comments = ticket.Comments.AsQueryable().Select(CommentsViewModel.FromComment).ToList()
                };
            }
        }

         public string PriorityAsString
         {
             get 
             {
                 return this.Priority.ToString();
             }
         }
    }
}