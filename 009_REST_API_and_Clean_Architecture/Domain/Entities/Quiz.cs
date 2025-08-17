using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public required string Topic { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public required ICollection<QuestionItem> Questions { get; set; }
    }
}
