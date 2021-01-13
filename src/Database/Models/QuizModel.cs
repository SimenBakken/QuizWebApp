using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class QuizModel : IdentityModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
