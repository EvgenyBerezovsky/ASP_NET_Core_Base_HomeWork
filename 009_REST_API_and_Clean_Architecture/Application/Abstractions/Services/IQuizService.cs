using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IQuizService
    {
        public List<GetQuizDTO> GetAll();
        public GetQuizDTO Create(CreateQuizDTO newQuizDto);
        public GetQuizDTO Delete(DeleteQuizDTO deletedQuizDto);
        public GetQuizDTO UpdateIsActive(UpdateIsActiveQuizDTO updatedQuizDto);
        public GetQuizDTO UpdateIsPublished(UpdateIsPublishedQuizDTO updatedQuizDto);
        public GetQuizDTO UpdateQuestions(UpdateQuestionsQuizDTO updatedQuizDto);
        public GetQuizDTO UpdateTopic(UpdateTopicQuizDTO updatedQuizDto);
    }
}
