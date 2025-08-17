using Microsoft.AspNetCore.Mvc;
using Lesson007.Abstractions;

namespace Task001.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogServiceController : ControllerBase
    {
        private readonly ILogService _logService;
        public LogServiceController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("LogDebug")]
        public ActionResult<bool> LogDebug()
        {
            var result = _logService.LogDebug();
            return Ok(result);
        }

        [HttpGet("LogIError")]
        public ActionResult<bool> LogIError()
        {
            var result = _logService.LogIError();
            return Ok(result);
        }

        [HttpGet("LogInformation")]
        public ActionResult<bool> LogInformation()
        {
            var result = _logService.LogInformation();
            return Ok(result);
        }

        [HttpGet("LogWarning")]
        public ActionResult<bool> LogWarning()
        {
            var result = _logService.LogWarning();
            return Ok(result);
        }
    }
}
