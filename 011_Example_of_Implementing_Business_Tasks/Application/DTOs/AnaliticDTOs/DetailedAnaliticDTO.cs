using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AnaliticDTOs
{
    public class DetailedAnaliticDTO
    {
        public DateRangeDTO Period { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CategoryAnalyticsDTO> Categories { get; set; } = new();
    }
}
