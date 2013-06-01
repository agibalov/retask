using System;

namespace Service.Exceptions
{
    public class TodoException : Exception
    {
        public ServiceError ServiceError { get; set; }

        public TodoException(ServiceError serviceError)
        {
            ServiceError = serviceError;
        }
    }
}