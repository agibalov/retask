using System.Linq;
using FluentValidation;
using Service.DAL;
using Service.Exceptions;
using Service.Validation.Requests;
using Service.Validation.Validators;

namespace Service.TransactionScripts
{
    public class ResetPasswordTransactionScript
    {
        private readonly ResetPasswordRequestValidator _resetPasswordRequestValidator;

        public ResetPasswordTransactionScript(ResetPasswordRequestValidator resetPasswordRequestValidator)
        {
            _resetPasswordRequestValidator = resetPasswordRequestValidator;
        }

        public void ResetPassword(TodoContext context, ResetPasswordRequest resetPasswordRequest)
        {
            _resetPasswordRequestValidator.ValidateAndThrow(resetPasswordRequest);

            var pendingPasswordReset = context
                .PendingPasswordResets
                .SingleOrDefault(x => x.Secret == resetPasswordRequest.Secret);
            if (pendingPasswordReset == null)
            {
                throw new TodoException(ServiceError.NoSuchPendingPasswordResetRequest);
            }

            var user = context.Users.Single(x => x.UserId == pendingPasswordReset.UserId);
            user.Password = resetPasswordRequest.Password;

            context.PendingPasswordResets.Remove(pendingPasswordReset);
            context.SaveChanges();
        }
    }
}