using FluentValidation;
using Service.DAL;
using Service.Exceptions;
using Service.TransactionScripts.BL;
using Service.Validation.Requests;
using Service.Validation.Validators;

namespace Service.TransactionScripts
{
    public class ChangePasswordTransactionScript
    {
        private readonly ChangePasswordRequestValidator _changePasswordRequestValidator;
        private readonly AuthenticationService _authenticationService;

        public ChangePasswordTransactionScript(
            ChangePasswordRequestValidator changePasswordRequestValidator,
            AuthenticationService authenticationService)
        {
            _changePasswordRequestValidator = changePasswordRequestValidator;
            _authenticationService = authenticationService;
        }

        public void ChangePassword(TodoContext context, ChangePasswordRequest changePasswordRequest)
        {
            _changePasswordRequestValidator.ValidateAndThrow(changePasswordRequest);

            var user = _authenticationService.GetUserBySessionToken(
                context, 
                changePasswordRequest.SessionToken);

            if (user.Password != changePasswordRequest.CurrentPassword)
            {
                throw new TodoException(ServiceError.InvalidPassword);
            }

            user.Password = changePasswordRequest.NewPassword;

            context.SaveChanges();
        }
    }
}