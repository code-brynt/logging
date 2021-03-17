using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Events;

namespace logging
{
    public class SecurityEventFilter : ILogEventFilter
    {
        public const string SECURITY_EVENT = "SecurityEvent";
        public bool IsEnabled(LogEvent logEvent)
        {
            if (logEvent.Properties.ContainsKey(nameof(EventId)))
            {
                var propsValues = logEvent.Properties[nameof(EventId)] as LogEventPropertyValue;
                return propsValues.ToString().Contains(SECURITY_EVENT);
            }
            else
            {
                return false;
            }

        }
    }
}
