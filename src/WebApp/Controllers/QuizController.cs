using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.DataLayer;
using Database.DataModels;
using Database.Models;
using Database.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly QuizDbContext _context;

        public QuizController(QuizDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("")]
        public IActionResult GetAllQuizzes()
        {
            return new JsonResult(QuizData.GetAllQuizzes(_context));
        }

        /// <summary>
        /// Get a quiz by id
        /// </summary>
        /// <param name="quizId">Id of quiz to get</param>
        /// <param name="showAnswers">Show answers to quiz, default: false</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{quizId:int}")]
        public IActionResult GetQuiz(int quizId, bool showAnswers = false)
        {
            // Get quiz
            FullQuizModel quiz = QuizData.GetQuiz(_context, quizId);
            if (quiz != null)
            {
                // Make sure all answers are false so client cant find answer in object before submitting their answer
                quiz?.Questions.SelectMany(x => x.Answers).ToList().ForEach(x => x.Answer.Correct = false);
                return new JsonResult(quiz);
            }

            return new NotFoundResult();
        }

        /// <summary>
        /// Check a quiz for correct answers
        /// </summary>
        /// <param name="quizId">Id of quiz to check</param>
        /// <param name="model">Quiz to check answers in</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{quizId:int}")]
        public IActionResult CheckQuiz(int quizId, [FromBody] FullQuizModel model)
        {
            // Give bad request if quiz is not sent in body
            if (model == null)
                return new BadRequestResult();

            // Should be replaced with getting only question answers instead of full quiz in future
            FullQuizModel quiz = QuizData.GetQuiz(_context, quizId);

            if (quiz != null)
            {
                // Loop through all questions
                foreach (var question in quiz.Questions)
                {
                    // Default to correct, is set to false if any mistakes are found later
                    question.Correct = true;

                    // Get all answers from correct question
                    IEnumerable<FullAnswerModel> answers = model.Questions.FirstOrDefault(x => x.Question.Id == question.Question.Id)?.Answers.Where(x => x.Selected);

                    if (answers != null)
                    {
                        // Set the corresponding answers in returned quiz as selected if they are selected in the sent in quiz
                        foreach (var answer in question.Answers)
                        {
                            answer.Selected = answers.Any(x => x.Answer.Id == answer.Answer.Id);
                        }

                        // Do correct check based on quiz type
                        switch ((TypeIdEnum)question.Question.TypeId)
                        {
                            case TypeIdEnum.MultipleChoice:
                                // If there are no selected answers or the selected answer is not correct, set correct to false
                                if (answers.Count() != 1)
                                    question.Correct = false;
                                else
                                {
                                    if (question.Answers.FirstOrDefault(x => x.Answer.Correct).Answer.Id != answers.FirstOrDefault().Answer.Id)
                                        question.Correct = false;
                                }
                                break;
                            case TypeIdEnum.MultipleCorrect:
                                // If there are no selected answers or any of the selected answers are not correct, set correct to false
                                if (answers.Count() < 1)
                                    question.Correct = false;
                                else
                                {
                                    // Get a list of correct answer ids and check that all supplied answers are in this list of solution ids
                                    var solution = question.Answers.Where(x => x.Answer.Correct).Select(x => x.Answer.Id).ToList();
                                    if (answers.Any(x => !solution.Contains(x.Answer.Id)))
                                        question.Correct = false;
                                }
                                break;
                            default:
                                question.Correct = false;
                                break;
                        }
                    }
                    else
                    {
                        question.Correct = false;
                    }
                }

                return new JsonResult(quiz);
            }

            return new NotFoundResult();
        }
    }
}
