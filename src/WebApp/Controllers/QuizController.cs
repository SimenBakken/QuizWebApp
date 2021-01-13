using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.DataLayer;
using Database.DataModels;
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
        public FullQuizModel GetQuiz(int quizId)
        {
            return QuizData.GetQuiz(_context, quizId);
        }
    }
}
