using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetScoreDTO
    {
        public DateTime Time { get; set; }
        public required string Topic { get; set; }
        public float Result { get; set; }
    }
}
