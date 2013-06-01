using System;
using Service.TransactionScripts.BL;

namespace ServiceTests.Stubs
{
    public class TestTimeProvider : ITimeProvider
    {
        private DateTime? _dateTime;

        public DateTime GetCurrentTime()
        {
            if (!_dateTime.HasValue)
            {
                throw new Exception("Current time not set");
            }

            return _dateTime.Value;
        }

        public void SetCurrentTime(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public void TickSeconds(int seconds)
        {
            var currentTime = GetCurrentTime();
            _dateTime = currentTime.AddSeconds(seconds);
        }

        public void Reset()
        {
            _dateTime = null;
        }
    }
}