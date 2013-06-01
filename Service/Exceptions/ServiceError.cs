namespace Service.Exceptions
{
    public enum ServiceError
    {
        InternalError = 1,
        NoSuchUser = 2,
        InvalidPassword = 3,
        UserAlreadyRegistered = 4,
        SessionExpired = 5,
        NoSuchTask = 6,
        NoPermissions = 7,
        InvalidTaskStatus = 8,
        ValidationError = 9,
        NoSuchPendingUser = 10,
        NoSuchPendingPasswordResetRequest = 11,
    }
}