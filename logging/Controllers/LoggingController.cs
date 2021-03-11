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
        LoggingLevelSwitch _levelSwitch;

        public LoggingController(ILogger<LoggingController> logger, LoggingLevelSwitch levelSwitch)
        {
            _logger = logger;
            _levelSwitch = levelSwitch;
        }

        [HttpGet]
        public void Get(LogEventLevel eventLevel)
        {
            _logger.LogDebug(_levelSwitch.MinimumLevel.ToString());
            _logger.LogDebug(eventLevel.ToString());

            _levelSwitch.MinimumLevel = eventLevel;
        }
    }
}
