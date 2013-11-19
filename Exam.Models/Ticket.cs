using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
       
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }
       
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [NotContainBug(ErrorMessage = "The word 'bug' should not be in the title")]
        public string  Title { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ScreenShotUrl { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Ticket()
        {
            this.Comments = new HashSet<Comment>();
            this.Priority = Priority.Medium;
        }
    }
}
