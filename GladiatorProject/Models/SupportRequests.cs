﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GladiatorProject.Models
{
    public class SupportRequests
    {
        [Key]
        public int Id { get; set; }

        public string User { get; set; }

        public string Email { get; set; }

        [MaxLength(25)]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""\s]+$")]
        public string Gladiator { get; set; }

        [Required]
        [RegularExpression(@"^[^<>?;:'()!~%\-_@#/*""]+$")]
        public string Request { get; set; }

        [Required]
        [RegularExpression(@"^[^<>?;:'()!~%\-_@#/*""]+$")]
        public string Details { get; set; }

        public string Solved { get; set; }

        public DateTime Date { get; set; }

    }
}