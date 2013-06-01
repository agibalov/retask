using System.Linq;
using FluentValidation;
using Service.DAL;
using Service.Exceptions;
using Service.Infrastructure.Notifiers;
using Service.TransactionScripts.BL;
using Service.Validation.Requests;
using Service.Validation.Validators;

namespace Service.TransactionScripts
{
    public class RequestPasswordResetTransactionScript
    {
        private readonly RequestPasswordResetRequestValidator _requestPasswordResetRequestValidator;
        private readonly SecretGenerator _secretGenerator;
        private readonly IResetPasswordNotifier _resetPasswordNotifier;

        public RequestPasswordResetTransactionScript(
            RequestPasswordResetRequestValidator requestPasswordResetRequestValidator,
            SecretGenerator secretGenerator,
            IResetPasswordNotifier resetPasswordNotifier)
        {
            _requestPasswordResetRequestValidator = requestPasswordResetRequestValidator;
            _secretGenerator = secretGenerator;
            _resetPasswordNotifier = resetPasswordNotifier;
        }

        public void RequestPasswordReset(TodoContext context, RequestPasswordResetRequest requestPasswordResetRequest)
        {
            _requestPasswordResetRequestValidator.ValidateAndThrow(requestPasswordResetRequest);

            var email = requestPasswordResetRequest.Email;
            var user = context.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new TodoException(ServiceError.NoSuchUser);
            }

            var resetPasswordRequestSecret = _secretGenerator.GenerateSecret();
            var pendingPasswordReset = new PendingPasswordReset
                {
                    UserId = user.UserId,
                    Secret = resetPasswordRequestSecret
                };
            context.PendingPasswordResets.Add(pendingPasswordReset);
            context.SaveChanges();

            _resetPasswordNotifier.Notify(email, resetPasswordRequestSecret);
        }
    }
}