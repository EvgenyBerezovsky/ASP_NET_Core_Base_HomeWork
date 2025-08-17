using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Lesson007.Abstractions;
using Lesson007.Exeptions;
using Lesson007.Models;

namespace Lesson007.Services
{
    public class LogService : ILogService
    {
        private int callCount = 3;
        private TimeSpan interval = TimeSpan.FromMinutes(1);
        private readonly IMemoryCache _cache;

        public LogService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool LogDebug()
        {
            if (!CountMethodCall(nameof(LogDebug))) 
                throw new LogServiceDebugException($"The number of calls to the method {nameof(LogDebug)} was more than 3");

            return true; 
        }

        public bool LogIError()
        {
            if (!CountMethodCall(nameof(LogIError))) 
                throw new LogServiceErrorException($"The number of calls to the method {nameof(LogIError)} was more than 3");

            return true;
        }

        public bool LogInformation()
        {
            if (!CountMethodCall(nameof(LogInformation))) 
                throw new LogServiceInformationException($"The number of calls to the method {nameof(LogInformation)} was more than 3  ");

            return true;
        }

        public bool LogWarning()
        {
            if (!CountMethodCall(nameof(LogWarning))) 
                throw new LogServiceWarningException($"The number of calls to the method {nameof(LogWarning)} was more than 3");

            return true;
        }

        /// <summary>
        /// Counts the number of method calls over a specified time interval.
        /// </summary>
        /// <param name="methodName"> Method name</param>
        /// <returns>
        /// true if the method has been called no more than a certain number of times
        /// in a certain period of time. Otherwise, false.
        /// </returns>
        private bool CountMethodCall(string methodName)
        {
            string сashKey = methodName;

            if (!_cache.TryGetValue(сashKey, out CallState? callState))
            {
                _cache.Set(сashKey, new CallState(1, DateTime.Now), interval);
                return true;
            }

            callState.Count++;

            if (callState.Count <= callCount)
            {
                return true;
            }
            return false;
        }
    }
}
