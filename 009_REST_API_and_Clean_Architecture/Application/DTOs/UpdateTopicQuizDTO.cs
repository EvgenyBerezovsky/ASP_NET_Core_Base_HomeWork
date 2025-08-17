using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateTopicQuizDTO
    {
        public required string Topic { get; set; }
        public required string NewTopic { get; set; }
    }
}
