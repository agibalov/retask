using System;

namespace Service.TransactionScripts.BL
{
    public class UtcNowTimeProvider : ITimeProvider
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }
    }
}