using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Exam.Web.Models
{
    public class CommentsViewModel
    {
        public int Id { get; set; }
        public string Author { get; set; }       
        public string Content { get; set; }

        public static Expression<Func<Comment, CommentsViewModel>> FromComment
        {
            get
            {
                return comment => new CommentsViewModel
                {
                    Id = comment.Id,
                    Author = comment.Author.UserName,
                    Content = comment.Content
                };
            }
        }        
    }
}