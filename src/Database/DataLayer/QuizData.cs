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
        /// <summary>
        /// Get a list of all Quizzes in the database
        /// </summary>
        /// <param name="context">Database context</param>
        /// <returns></returns>
        public static IEnumerable<QuizModel> GetAllQuizzes(QuizDbContext context)
        {
            return context.Quizzes;
        }

        /// <summary>
        /// Get a quiz
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="quizId">Id of quiz to get</param>
        /// <returns></returns>
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
                        Answers = context.AnswersToQuestions.Where(y => y.QuestionId == x.QuestionId).Select(y => new FullAnswerModel() { Answer = context.Answers.FirstOrDefault(z => z.Id == y.AnswerId) }).ToList()
                    }
                ).ToList();

                return fullQuiz;
            }

            return null;
        }
    }
}
