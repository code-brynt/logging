using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Events;

namespace logging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger<LoggingController> _logger;
        private readonly LoggingLevelSwitch _levelSwitch;

        public LoggingController(ILogger<LoggingController> logger, LoggingLevelSwitch levelSwitch)
        {
            _logger = logger;
            _levelSwitch = levelSwitch;
        }

        [HttpPut("logLevel")]
        public IActionResult ChangeLevel(LogEventLevel eventLevel)
        {
            // change the injected logging level
            _levelSwitch.MinimumLevel = eventLevel;

            return Ok();
        }

        [HttpGet("LogEvent")]
        public IActionResult LogEvent()
        {
            _logger.LogSecurityEvent("log event raised by the controller");

            return Ok();
        }
    }
}
