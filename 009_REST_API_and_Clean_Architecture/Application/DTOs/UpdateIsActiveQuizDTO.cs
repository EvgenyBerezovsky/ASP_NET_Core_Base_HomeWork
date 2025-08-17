using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateIsActiveQuizDTO
    {
        public required string Topic { get; set; }
        public bool IsActive { get; set; }
    }
}
