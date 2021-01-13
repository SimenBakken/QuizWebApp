using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class AnswerModel : IdentityModel
    {
        [Required]
        public string Answer { get; set; }
        [Required]
        public bool Correct { get; set; }
    }
}
