using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class QuizService : IQuizService
    {
        private readonly IDataRepository _dataRepository;
        public QuizService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }
        
        public List<GetQuizDTO> GetAll()
        {
            var quizzes = _dataRepository.GetAllQuizzes();

            if (quizzes == null || !quizzes.Any()) 
                return new List<GetQuizDTO>();

            return quizzes.Select(q => MapQuizToGetQuizDTO(q)).ToList();
        }

        public GetQuizDTO Create(CreateQuizDTO newQuizDto)
        {
            Quiz quiz = new()
            {
                Topic = newQuizDto.Topic,
                IsActive = true,
                IsPublished = true,
                Questions = newQuizDto.Questions.Select(q => new QuestionItem
                {
                    Question = q.Question,
                    Answer = q.Answer,
                    CorrectOptionIndex = q.CorrectOptionIndex,
                    Options = new Options
                    {
                        Option1 = q.Options.Option1,
                        Option2 = q.Options.Option2,
                        Option3 = q.Options.Option3,
                        Option4 = q.Options.Option4
                    }
                }).ToList()
            };

            var createdQuiz = _dataRepository.CreateQuiz(quiz);

            if (createdQuiz == null)
            {
                throw new InvalidOperationException("Quiz creation failed.");
            }

            return MapQuizToGetQuizDTO(createdQuiz);
        }

        public GetQuizDTO Delete(DeleteQuizDTO deletedQuizDto)
        {
            var topic = deletedQuizDto.Topic;
            var quiz = _dataRepository.GetQuizByTopic(topic);

            if (quiz is null)
            {
                throw new InvalidOperationException($"Quiz with topic '{topic}' does not exist.");
            }

            var deletedQuiz = _dataRepository.DeleteQuiz(quiz);

            if (deletedQuiz == null)
            {
                throw new InvalidOperationException("Quiz deletion failed.");
            }

            return MapQuizToGetQuizDTO(deletedQuiz);
        }

        public GetQuizDTO UpdateTopic(UpdateTopicQuizDTO updatedQuizDto)
        {
            var topic = updatedQuizDto.Topic;
            var quiz = _dataRepository.GetQuizByTopic(topic);

            if (quiz is null)
            {
                throw new InvalidOperationException($"Quiz with topic '{topic}' does not exist.");
            }

            quiz.Topic = updatedQuizDto.NewTopic;
            var updatedQuiz = _dataRepository.UpdateQuiz(quiz);

            if (updatedQuiz == null)
            {
                throw new InvalidOperationException("Quiz update failed.");
            }

            return MapQuizToGetQuizDTO(updatedQuiz);
        }

        public GetQuizDTO UpdateIsActive(UpdateIsActiveQuizDTO updatedQuizDto)
        {
            var topic = updatedQuizDto.Topic;
            var quiz = _dataRepository.GetQuizByTopic(topic);

            if (quiz is null)
            {
                throw new InvalidOperationException($"Quiz with topic '{topic}' does not exist.");
            }

            quiz.IsActive = updatedQuizDto.IsActive;
            var updatedQuiz = _dataRepository.UpdateQuiz(quiz);

            if (updatedQuiz == null)
            {
                throw new InvalidOperationException("Quiz update failed.");
            }

            return MapQuizToGetQuizDTO(updatedQuiz);
        }

        public GetQuizDTO UpdateQuestions(UpdateQuestionsQuizDTO updatedQuizDto)
        {
            var topic = updatedQuizDto.Topic;
            var quiz = _dataRepository.GetQuizByTopic(topic);

            if (quiz is null)
            {
                throw new InvalidOperationException($"Quiz with topic '{topic}' does not exist.");
            }

            quiz.Questions = updatedQuizDto.Questions;
            var updatedQuiz = _dataRepository.UpdateQuiz(quiz);

            if (updatedQuiz == null)
            {
                throw new InvalidOperationException("Quiz update failed.");
            }

            return MapQuizToGetQuizDTO(updatedQuiz);
        }

        public GetQuizDTO UpdateIsPublished(UpdateIsPublishedQuizDTO updatedQuizDto)
        {
            var topic = updatedQuizDto.Topic;
            var quiz = _dataRepository.GetQuizByTopic(topic);

            if (quiz is null)
            {
                throw new InvalidOperationException($"Quiz with topic '{topic}' does not exist.");
            }

            quiz.IsActive = updatedQuizDto.IsPublished;
            var updatedQuiz = _dataRepository.UpdateQuiz(quiz);

            if (updatedQuiz == null)
            {
                throw new InvalidOperationException("Quiz update failed.");
            }

            return MapQuizToGetQuizDTO(updatedQuiz);
        }

        private GetQuizDTO MapQuizToGetQuizDTO(Quiz quiz)
        {
            return new GetQuizDTO
            {
                Topic = quiz.Topic,
                IsActive = quiz.IsActive,
                IsPublished = quiz.IsPublished,
                Questions = quiz.Questions.Select(q => new GetQuestionItemDTO
                {
                    Question = q.Question,
                    Answer = q.Answer,
                    CorrectOptionIndex = q.CorrectOptionIndex,
                    Options = new GetOptionsDTO
                    {
                        Option1 = q.Options.Option1,
                        Option2 = q.Options.Option2,
                        Option3 = q.Options.Option3,
                        Option4 = q.Options.Option4
                    }
                }).ToList()
            };
        }
    }
}
