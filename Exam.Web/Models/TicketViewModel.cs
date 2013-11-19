using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Exam.Web.Models
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string Author { get; set; }     
        public string Category { get; set; }      
        public string Title { get; set; }

        public Priority Priority { get; set; }
        public int CommentsCount  { get; set; }

        public static Expression<Func<Ticket, TicketViewModel>> FromTicket
        {
            get
            {
                return ticket => new TicketViewModel
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    Author = ticket.Author.UserName,
                    Category = ticket.Category.Name,
                    Priority = ticket.Priority,
                    CommentsCount = ticket.Comments.Count()                   
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