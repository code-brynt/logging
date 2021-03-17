using Microsoft.Extensions.Logging;

namespace logging
{
    public static class LoggerExtensions
    {
        public static void LogSecurityEvent(this ILogger logger, string message)
        {
            // use an EventId that can be found by he SecuriytEventFilter
            var eventId = new EventId(int.MaxValue, SecurityEventFilter.SECURITY_EVENT);
            logger.LogInformation(eventId, message);
        }
    }
}
