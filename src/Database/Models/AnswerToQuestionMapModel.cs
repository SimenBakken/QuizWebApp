using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models
{
    public class AnswerToQuestionMapModel
    {
        [Key]
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public QuestionModel Question { get; set; }

        [Key]
        public int AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        public AnswerModel Answer { get; set; }
    }
}
