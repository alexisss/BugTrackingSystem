namespace Exam.Data.Migrations
{
    using Exam.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DataContext context)
        {            
                if (context.Tickets.Count() > 0)
                {
                    return;
                }

           
                Random rand = new Random();

                Category category = new Category() { Name = "Important" };
                ApplicationUser user = new ApplicationUser() { UserName = "TestUser" };

                var userAdmin = new ApplicationUser()
                {
                    UserName = "admin",                  
                    Logins = new Collection<UserLogin>()
                    {
                        new UserLogin()
                        {
                            LoginProvider = "Local",
                            ProviderKey = "admin",
                        }
                    },
                    Roles = new Collection<UserRole>()
                    {
                        new UserRole()
                        {
                            Role = new Role("Admin")
                        }
                    }
                };

            context.Users.Add(userAdmin);
            context.UserSecrets.Add(new UserSecret("admin",
                "ACQbq83L/rsvlWq11Zor2jVtz2KAMcHNd6x1SN2EXHs7VuZPGaE8DhhnvtyO10Nf5Q=="));//admin123

                for (int i = 0; i < 10; i++)
                {
                    Ticket ticket = new Ticket();
                    ticket.Author = user;
                    ticket.Title = "Title" + rand.Next(10, 1000);
                    ticket.Priority = Priority.Medium;
                    ticket.Category = category;

                    var commentsCount = rand.Next(0, 10);
                    for (int j = 0; j < commentsCount; j++)
                    {
                        ticket.Comments.Add(new Comment { Content = "Perfect", Author = user });
                    }


                    context.Tickets.Add(ticket);
                }              

                context.SaveChanges();

                base.Seed(context);
            }
        }
    }


