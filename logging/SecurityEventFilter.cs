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
            // does the entry have an EventId
            if (logEvent.Properties.ContainsKey(nameof(EventId)))
            {
                var propsValues = logEvent.Properties[nameof(EventId)] as LogEventPropertyValue;
                // is the EventId a Security Event
                return propsValues.ToString().Contains(SECURITY_EVENT);
            }
            else
            {
                return false;
            }

        }
    }
}
