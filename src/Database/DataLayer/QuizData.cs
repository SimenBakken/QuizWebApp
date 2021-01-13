using Database.DataModels;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.DataLayer
{
    public static class QuizData
    {
        public static FullQuizModel GetQuiz(QuizDbContext context, int quizId)
        {
            QuizModel quiz = context.Quizzes.FirstOrDefault(x => x.Id == quizId);

            if (quiz != null)
            {
                FullQuizModel fullQuiz = new FullQuizModel() { Quiz = quiz };

                fullQuiz.Questions = context.QuestionsToQuizzes.Where(x => x.QuizId == quizId).Select(x =>
                    new FullQuestionModel()
                    {
                        Question = context.Questions.FirstOrDefault(y => y.Id == x.QuestionId),
                        Answers = context.AnswersToQuestions.Where(y => y.QuestionId == x.QuestionId).Select(y => context.Answers.FirstOrDefault(z => z.Id == y.AnswerId)).AsEnumerable()
                    }
                );

                return fullQuiz;
            }

            return null;
        }
    }
}
