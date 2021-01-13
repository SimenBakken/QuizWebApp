using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DataModels
{
    public class FullQuestionModel
    {
        public QuestionModel Question { get; set; }
        public IEnumerable<AnswerModel> Answers { get; set; }
    }
}
