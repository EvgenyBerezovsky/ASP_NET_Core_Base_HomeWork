using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuestionItem
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int CorrectOptionIndex { get; set; }
        public int OptionsId { get; set; }
        public required Options Options { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
