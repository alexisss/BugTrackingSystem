using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Exam.Web.Models
{
    public class CategoriesViewModel
    {
        
        public int Id { get; set; }
                
        public string Name { get; set; }

        public static Expression<Func<Category, CategoriesViewModel>> FromCategory
        {
            get
            {
                return category => new CategoriesViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                };
            }
        }        
    }
}