using Database.Models;
using Database.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class QuizDbContext : DbContext
    {
        public DbSet<AnswerModel> Answers { get; set; }
        public DbSet<AnswerToQuestionMapModel> AnswersToQuestions { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<QuestionToQuizMapModel> QuestionsToQuizzes { get; set; }
        public DbSet<QuestionTypeModel> QuestionTypes { get; set; }
        public DbSet<QuizModel> Quizzes { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="options"></param>
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Seed data on database create
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnswerToQuestionMapModel>().HasKey("AnswerId", "QuestionId");
            modelBuilder.Entity<QuestionToQuizMapModel>().HasKey("QuizId", "QuestionId");

            modelBuilder.Entity<QuizModel>().HasData(
                new QuizModel
                {
                    Id = 1,
                    Name = "Demo quiz",
                    Description = "Demonstration quiz to show the web application working"
                }
            );

            modelBuilder.Entity<QuestionTypeModel>().HasData(
                new QuestionTypeModel
                {
                    Id = (int)TypeIdEnum.MultipleChoice,
                    Type = "Multiple choice",
                    Description = "Give multiple options, but only one is correct"
                },
                new QuestionTypeModel
                {
                    Id = (int)TypeIdEnum.MultipleCorrect,
                    Type = "Multiple correct",
                    Description = "Give multiple options, multiple can be correct"
                }
            );

            modelBuilder.Entity<QuestionModel>().HasData(
                new QuestionModel
                {
                    Id = 1,
                    Question = "What sound does a dog make?",
                    TypeId = 1
                },
                new QuestionModel
                {
                    Id = 2,
                    Question = "What is 8 + 8?",
                    TypeId = 2
                }
            );

            modelBuilder.Entity<AnswerModel>().HasData(
                new AnswerModel
                {
                    Id = 1,
                    Answer = "Meow",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 2,
                    Answer = "Mooo",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 3,
                    Answer = "Oink",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 4,
                    Answer = "Woof",
                    Correct = true
                },
                new AnswerModel
                {
                    Id = 5,
                    Answer = "Chrip",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 6,
                    Answer = "5",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 7,
                    Answer = "16",
                    Correct = true
                },
                new AnswerModel
                {
                    Id = 8,
                    Answer = "Sixteen",
                    Correct = true
                },
                new AnswerModel
                {
                    Id = 9,
                    Answer = "Twelve",
                    Correct = false
                }
            );

            modelBuilder.Entity<AnswerToQuestionMapModel>().HasData(
                new AnswerToQuestionMapModel
                {
                    QuestionId = 1,
                    AnswerId = 1
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 1,
                    AnswerId = 2
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 1,
                    AnswerId = 3
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 1,
                    AnswerId = 4
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 1,
                    AnswerId = 5
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 2,
                    AnswerId = 6
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 2,
                    AnswerId = 7
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 2,
                    AnswerId = 8
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 2,
                    AnswerId = 9
                }
            );

            modelBuilder.Entity<QuestionToQuizMapModel>().HasData(
                new QuestionToQuizMapModel
                {
                    QuizId = 1,
                    QuestionId = 1
                },
                new QuestionToQuizMapModel
                {
                    QuizId = 1,
                    QuestionId = 2
                }
            );
        }
    }
}
