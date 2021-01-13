using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class QuestionTypeModel : IdentityModel
    {
        [Required]
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
