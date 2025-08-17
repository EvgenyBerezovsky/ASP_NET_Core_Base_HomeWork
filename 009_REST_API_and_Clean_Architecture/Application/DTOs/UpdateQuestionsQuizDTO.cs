    using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateQuestionsQuizDTO
    {
        public required string Topic { get; set; }
        public required ICollection<QuestionItem> Questions { get; set; }
    }
}
