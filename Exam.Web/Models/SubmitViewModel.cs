using Exam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam.Web.Models
{
    public class SubmitViewModel
    {
        [Required]
        [NotContainBug(ErrorMessage="The word bug is unacceptable")]
        public string Description { get; set; }

        [Required]
        public int TicketId { get; set; }
    }
}