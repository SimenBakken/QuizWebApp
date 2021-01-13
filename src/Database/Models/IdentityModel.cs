using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class IdentityModel
    {
        [Key]
        public int Id { get; set; }
    }
}
