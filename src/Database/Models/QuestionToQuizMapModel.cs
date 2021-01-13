using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models
{
    public class QuestionToQuizMapModel
    {        
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public QuestionModel Question { get; set; }

        public int QuizId { get; set; }
        [ForeignKey("QuizId")]
        public QuizModel Quiz { get; set; }
    }
}
