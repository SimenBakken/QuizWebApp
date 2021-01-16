using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DataModels
{
    public class FullQuestionModel
    {
        public bool Correct { get; set; }
        public QuestionModel Question { get; set; }
        public List<FullAnswerModel> Answers { get; set; }
    }
}
