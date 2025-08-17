using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetQuestionItemDTO
    {
        public required string Question { get; set; }
        public required string Answer { get; set; }
        public int CorrectOptionIndex { get; set; }
        public required GetOptionsDTO Options { get; set; }
    }
}
