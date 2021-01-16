using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DataModels
{
    public class FullAnswerModel
    {
        public AnswerModel Answer { get; set; }
        public bool Selected { get; set; }
    }
}
