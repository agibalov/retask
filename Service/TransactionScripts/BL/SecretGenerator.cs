using System;

namespace Service.TransactionScripts.BL
{
    public class SecretGenerator
    {
        public string GenerateSecret()
        {
            return Guid.NewGuid().ToString();
        }
    }
}