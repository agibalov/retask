using System;

namespace Service.TransactionScripts.BL
{
    public interface ITimeProvider
    {
        DateTime GetCurrentTime();
    }
}