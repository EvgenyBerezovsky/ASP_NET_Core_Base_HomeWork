using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AnaliticDTOs
{
    public class CategoryAnalyticsDTO
    {
        public string? Name { get; set; }
        public decimal Amount { get; set; }
        public double Percentage { get; set; }
        public List<ExpenseItemDTO> Items { get; set; } = new();
    }
}

