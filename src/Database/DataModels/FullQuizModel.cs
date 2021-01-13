using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DataModels
{
    public class FullQuizModel
    {
        public QuizModel Quiz { get; set; }

        public IEnumerable<FullQuestionModel> Questions { get; set; }
    }
}
