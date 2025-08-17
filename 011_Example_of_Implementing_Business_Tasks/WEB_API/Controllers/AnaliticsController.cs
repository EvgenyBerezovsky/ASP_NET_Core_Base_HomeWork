using Application.Abstractions.Services;
using Application.DTOs.AnaliticDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnaliticsService _analyticsService;
        private readonly IUserContextService _userContext;

        public AnalyticsController(IAnaliticsService analyticsService, IUserContextService userContext)
        {
            _analyticsService = analyticsService;
            _userContext = userContext;
        }

        [HttpGet("detailed")]
        [Authorize]
        public async Task<ActionResult<DetailedAnaliticDTO>> GetDetailedAnalyticsAsync([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var login = _userContext.GetUserLogin(); 
            if (string.IsNullOrEmpty(login))
            {
                return Unauthorized("Не вдалося отримати логін користувача.");
            }

            if (from > to)
            {
                return BadRequest("Дата 'from' не може бути пізніше за 'to'.");
            }

            var analytics = await _analyticsService.GetDetailedAnalyticsAsync(login, new DateRangeDTO() { From = from, To = to});
            return Ok(analytics);
        }
    }
}
