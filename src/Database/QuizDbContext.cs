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
        // Define models in database context
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
        /// Seed data on database create and set keys
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set primary key for tables that have multiple columns as key
            modelBuilder.Entity<AnswerToQuestionMapModel>().HasKey("AnswerId", "QuestionId");
            modelBuilder.Entity<QuestionToQuizMapModel>().HasKey("QuizId", "QuestionId");

            // Seed example quiz on database create
            modelBuilder.Entity<QuizModel>().HasData(
                new QuizModel
                {
                    Id = 1,
                    Name = "Demo quiz",
                    Description = "Demonstration quiz with 4 questions to show the web application working"
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
                    Question = "How many blue stripes are there on the U.S. flag?",
                    TypeId = 1
                },
                new QuestionModel
                {
                    Id = 2,
                    Question = "What trees can you find in Norway? (one point for each correct, zero point if any wrong)",
                    TypeId = 2
                },
                new QuestionModel
                {
                    Id = 3,
                    Question = "In what year did the Wall Street Crash take place?",
                    TypeId = 1
                }, 
                new QuestionModel
                {
                    Id = 4,
                    Question = "Which country has appeared in three soccer World Cup finals but has never won?",
                    TypeId = 1
                }
            );

            modelBuilder.Entity<AnswerModel>().HasData(
                new AnswerModel
                {
                    Id = 1,
                    Answer = "6",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 2,
                    Answer = "7",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 3,
                    Answer = "13",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 4,
                    Answer = "0",
                    Correct = true
                },
                new AnswerModel
                {
                    Id = 5,
                    Answer = "Osp",
                    Correct = true
                },
                new AnswerModel
                {
                    Id = 6,
                    Answer = "Akasie",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 7,
                    Answer = "Or",
                    Correct = true
                },
                new AnswerModel
                {
                    Id = 8,
                    Answer = "Tuja",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 9,
                    Answer = "1929",
                    Correct = true
                },
                new AnswerModel
                {
                    Id = 10,
                    Answer = "1932",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 11,
                    Answer = "1930",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 12,
                    Answer = "1925",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 13,
                    Answer = "England",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 14,
                    Answer = "Spain",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 15,
                    Answer = "Uruguay",
                    Correct = false
                },
                new AnswerModel
                {
                    Id = 16,
                    Answer = "Netherlands",
                    Correct = true
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
                    QuestionId = 2,
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
                    QuestionId = 3,
                    AnswerId = 9
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 3,
                    AnswerId = 10
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 3,
                    AnswerId = 11
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 3,
                    AnswerId = 12
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 4,
                    AnswerId = 13
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 4,
                    AnswerId = 14
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 4,
                    AnswerId = 15
                },
                new AnswerToQuestionMapModel
                {
                    QuestionId = 4,
                    AnswerId = 16
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
                },
                new QuestionToQuizMapModel
                {
                    QuizId = 1,
                    QuestionId = 3
                },
                new QuestionToQuizMapModel
                {
                    QuizId = 1,
                    QuestionId = 4
                }
            );
        }
    }
}
