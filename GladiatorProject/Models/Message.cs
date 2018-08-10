using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GladiatorProject.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[^<>;:'()!~%\-_@#/*""]+$")]
        public string Body { get; set; }

        public string From { get; set; }

        public DateTime Sent { get; set; }

        [ForeignKey("Ticket")]
        public int SupportRequest_ID { get; set; }

        public SupportRequests Ticket { get; set; }
    }
}