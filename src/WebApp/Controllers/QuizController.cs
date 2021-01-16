using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.DataLayer;
using Database.DataModels;
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
        [Route("{quizId:int}")]
        public IActionResult GetQuiz(int quizId)
        {
            FullQuizModel quiz = QuizData.GetQuiz(_context, quizId);
            if (quiz != null)
            {
                // Make sure all answers are false so client cant find answer in object before submitting their answer
                quiz?.Questions.SelectMany(x => x.Answers).ToList().ForEach(x => x.Answer.Correct = false);
                return new JsonResult(quiz);
            }

            return new NotFoundResult();
        }

        [HttpPost]
        [Route("{quizId:int}")]
        public IActionResult CheckQuiz(int quizId, [FromBody] FullQuizModel model)
        {
            if (model == null)
                return new BadRequestResult();

            // Should be replaced with getting only question answers instead of full quiz in future
            FullQuizModel quiz = QuizData.GetQuiz(_context, quizId);

            if (quiz != null)
            {
                foreach (var question in quiz.Questions.ToList())
                {
                    question.Correct = true;
                    IEnumerable<FullAnswerModel> answers = model.Questions.FirstOrDefault(x => x.Question.Id == question.Question.Id)?.Answers.Where(x => x.Selected);

                    if (answers != null)
                    {
                        foreach (var answer in question.Answers)
                        {
                            answer.Selected = answers.Any(x => x.Answer.Id == answer.Answer.Id);
                        }
                        switch ((TypeIdEnum)question.Question.TypeId)
                        {
                            case TypeIdEnum.MultipleChoice:
                                if (answers.Count() != 1)
                                    question.Correct = false;
                                else
                                {
                                    if (question.Answers.FirstOrDefault(x => x.Answer.Correct).Answer.Id != answers.FirstOrDefault().Answer.Id)
                                        question.Correct = false;
                                }
                                break;
                            case TypeIdEnum.MultipleCorrect:
                                if (answers.Count() < 1)
                                    question.Correct = false;
                                else
                                {
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
