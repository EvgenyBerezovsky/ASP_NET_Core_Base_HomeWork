using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateIsPublishedQuizDTO
    {
        public required string Topic { get; set; }
        public bool IsPublished { get; set; }
    }
}
