using Exam.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Data
{
    public interface IUowData
    {
        IRepository<Ticket> Tickets { get; }

        IRepository<Category> Categories { get; }

        IRepository<ApplicationUser> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<Comment> Comments { get; }
        int SaveChanges();
    }
}
