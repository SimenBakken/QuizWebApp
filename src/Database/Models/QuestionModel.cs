using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class QuestionModel : IdentityModel
    {
        [Required]
        public string Question { get; set; }
        [Required]
        public int TypeId { get; set; }
    }
}
