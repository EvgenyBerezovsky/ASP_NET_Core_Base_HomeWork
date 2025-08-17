using Application.DTOs.AnaliticDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IAnaliticsService
    {
        public Task<DetailedAnaliticDTO> GetDetailedAnalyticsAsync(string login, DateRangeDTO dateRange);
    }
}
