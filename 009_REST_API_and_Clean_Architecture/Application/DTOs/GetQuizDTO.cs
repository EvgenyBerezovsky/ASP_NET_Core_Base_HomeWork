using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetQuizDTO
    {
        public required string Topic { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public required ICollection<GetQuestionItemDTO> Questions { get; set; }
    }
}
