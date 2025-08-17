using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.DTOs.AnaliticDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AnaliticsService : IAnaliticsService
    {
        private readonly IDataRepository _dataRepository;
        public AnaliticsService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }
        public async Task<DetailedAnaliticDTO> GetDetailedAnalyticsAsync(string login, DateRangeDTO dateRange)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);
            if (user == null)
            {
                throw new ArgumentException($"User with login {login} not found.");
            }

            var expenses = user.Expenses
                .Where(e => e.Date >= dateRange.From && e.Date <= dateRange.To)
                .ToList();

            var total = expenses.Sum(e => e.Amount);
            var categories = expenses.GroupBy(e => e.Category.Name)
                .Select(g => new CategoryAnalyticsDTO
                {
                    Name = g.Key,
                    Amount = g.Sum(e => e.Amount),
                    Percentage = total == 0 ? 0 : Math.Round((double)(g.Sum(e => e.Amount) / total * 100),2),
                    Items = g.Select(e => new ExpenseItemDTO
                    {
                        Description = e.Description,
                        Amount = e.Amount,
                        Date = e.Date
                    }).ToList()
                }).ToList();
            return new DetailedAnaliticDTO
            {
                Period = new DateRangeDTO { From = dateRange.From, To = dateRange.To },
                TotalAmount = total,
                Categories = categories
            };
        }
    }
}
