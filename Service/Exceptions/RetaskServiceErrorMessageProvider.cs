using System.Collections.Generic;

namespace Service.Exceptions
{
    public class RetaskServiceErrorMessageProvider
    {
        private readonly IDictionary<ServiceError, string> _messages = new Dictionary<ServiceError,string>();

        public RetaskServiceErrorMessageProvider()
        {
            _messages[ServiceError.InternalError] = "Internal error occured";
            _messages[ServiceError.NoSuchUser] = "There's no such user";
            _messages[ServiceError.InvalidPassword] = "Invalid password";
            _messages[ServiceError.UserAlreadyRegistered] = "User already registered";
            _messages[ServiceError.NoSuchTask] = "No such task";
            _messages[ServiceError.NoPermissions] = "No permissions";
            _messages[ServiceError.InvalidTaskStatus] = "Invalid task status";
            _messages[ServiceError.ValidationError] = "Validation error";
            _messages[ServiceError.NoSuchPendingUser] = "No such account activation request";
            _messages[ServiceError.NoSuchPendingPasswordResetRequest] = "No such password reset request";
        }

        public string GetErrorMessage(ServiceError serviceError)
        {
            if (!_messages.ContainsKey(serviceError))
            {
                return "Unknown error occured";
            }

            return _messages[serviceError];
        }
    }
}