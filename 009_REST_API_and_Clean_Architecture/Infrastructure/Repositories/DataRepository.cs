using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly ApplicationDbContext _context;
        public DataRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Quiz? CreateQuiz(Quiz quiz)
        {
            var existingQuiz = _context.Quizzes.FirstOrDefault(q => q.Topic == quiz.Topic);
            if (existingQuiz != null)
            {
                return null;
            }

            _context.Quizzes.Add(quiz);
            _context.SaveChanges();

            return quiz;
        }

        public Quiz? DeleteQuiz(Quiz quiz)
        {
            var deletedQuiz = _context.Quizzes.FirstOrDefault(q => q.Topic == quiz.Topic);
            if (deletedQuiz == null)
            {
                return null;
            }
            _context.Quizzes.Remove(deletedQuiz);
            _context.SaveChanges();
            return quiz;
        }

        public List<Quiz> GetAllQuizzes()
        {
            List<Quiz> quizzes = new();
            quizzes = _context.Quizzes.Include(q => q.Questions)
                                      .ThenInclude(q => q.Options)
                                      .ToList();
            return quizzes;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new();
            users = _context.Users.Include(u => u.Scores).ToList();
            return users;
        }

        public Quiz? GetQuizByTopic(string topic)
        {
            var quiz = _context.Quizzes.Include(q => q.Questions)
                                       .ThenInclude(q => q.Options)
                                       .FirstOrDefault(q => q.Topic == topic);
            if (quiz == null)
            {
                return null;
            }
            return quiz;
        }

        public User? GetUserByName(string name)
        {
            var user = _context.Users.Include(u => u.Scores)
                                     .FirstOrDefault(u => u.Name == name);
            return user;
        }

        public Quiz? UpdateQuiz(Quiz quiz)
        {
            var updatedQuiz = _context.Quizzes.Include(q => q.Questions)
                                              .ThenInclude(q => q.Options)
                                              .FirstOrDefault(q => q.Id == quiz.Id);

            if (updatedQuiz == null) return null;

            updatedQuiz.Topic = quiz.Topic;
            updatedQuiz.IsActive = quiz.IsActive;
            updatedQuiz.IsPublished = quiz.IsPublished;
           
            var existedQuestions = updatedQuiz.Questions.ToList();
            var newQuestions = quiz.Questions.ToList(); 

            foreach (var existedQuestion in existedQuestions)
            {
                if (!quiz.Questions.Any(q => q.Id == existedQuestion.Id))
                    _context.Questions.Remove(existedQuestion);
            }

            foreach (var newQuestion in newQuestions)
            {
                var existingQuestion = updatedQuiz.Questions.FirstOrDefault(q => q.Id == newQuestion.Id);
                if (existingQuestion != null)
                {
                    existingQuestion.Question = newQuestion.Question;
                    existingQuestion.Answer = newQuestion.Answer;
                    existingQuestion.CorrectOptionIndex = newQuestion.CorrectOptionIndex;
                    existingQuestion.Options.Option1 = newQuestion.Options.Option1;
                    existingQuestion.Options.Option2 = newQuestion.Options.Option2;
                    existingQuestion.Options.Option3 = newQuestion.Options.Option3;
                    existingQuestion.Options.Option4 = newQuestion.Options.Option4;
                }
                else
                {
                    updatedQuiz.Questions.Add(new QuestionItem
                    {
                        Question = newQuestion.Question,
                        Answer = newQuestion.Answer,
                        CorrectOptionIndex = newQuestion.CorrectOptionIndex,
                        Options = newQuestion.Options
                    });
                }
            }


            _context.Quizzes.Update(updatedQuiz);
            _context.SaveChanges();
            return updatedQuiz;
        }


    }
}
