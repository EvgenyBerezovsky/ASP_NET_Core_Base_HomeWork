using Application.Abstractions.Services;
using Application.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lesson009_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        public QuizController(IQuizService quizService)
        {
            _quizService = quizService ?? throw new ArgumentNullException(nameof(quizService));
        }

        [HttpGet("All")]
        public ActionResult<List<GetQuizDTO>> GetAll()
        {
            var quizzes = _quizService.GetAll();
            if (quizzes == null || !quizzes.Any())
            {
                return NotFound("No quizzes found.");
            }
            return Ok(quizzes);
        }

        [HttpPost]
        public ActionResult<GetQuizDTO> CreateQuiz(CreateQuizDTO quizDto)
        {
            return Ok(_quizService.Create(quizDto));
        }

        [HttpPatch("IsActive")]
        public ActionResult<GetQuizDTO> UpdateIsActiveQuiz(UpdateIsActiveQuizDTO quizDto)
        {
            return Ok(_quizService.UpdateIsActive(quizDto));
        }

        [HttpPatch("IsPublished")]
        public ActionResult<GetQuizDTO> UpdateIsPublishedQuiz(UpdateIsPublishedQuizDTO quizDto)
        {
            return Ok(_quizService.UpdateIsPublished(quizDto));
        }

        [HttpPatch("Topic")]
        public ActionResult<GetQuizDTO> UpdateTopicQuiz(UpdateTopicQuizDTO quizDto)
        {
            return Ok(_quizService.UpdateTopic(quizDto));
        }

        [HttpPatch("Questions")]
        public ActionResult<GetQuizDTO> UpdateQuestionsQuiz(UpdateQuestionsQuizDTO quizDto)
        {
            return Ok(_quizService.UpdateQuestions(quizDto));
        }

        [HttpDelete]
        public ActionResult<GetQuizDTO> DeleteQuiz(DeleteQuizDTO quizDto)
        {
            return Ok(_quizService.Delete(quizDto));
        }

        [HttpGet("{topic}")]
        public ActionResult<GetQuizDTO> GetQuizByTopic(string topic)
        {
            var quizzes = _quizService.GetAll();
            var quiz = quizzes.FirstOrDefault(q => q.Topic.Equals(topic, StringComparison.OrdinalIgnoreCase));
            if (quiz == null)
            {
                return NotFound($"Quiz with topic '{topic}' not found.");
            }
            return Ok(quiz);
        }
    }
}
