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
    public class SignUpTransactionScript
    {
        private readonly SignUpRequestValidator _signUpRequestValidator;
        private readonly ISignUpConfirmationNotifier _signUpConfirmationNotifier;
        private readonly SecretGenerator _secretGenerator;

        public SignUpTransactionScript(
            SignUpRequestValidator signUpRequestValidator,
            ISignUpConfirmationNotifier signUpConfirmationNotifier,
            SecretGenerator secretGenerator)
        {
            _signUpRequestValidator = signUpRequestValidator;
            _signUpConfirmationNotifier = signUpConfirmationNotifier;
            _secretGenerator = secretGenerator;
        }

        public void SignUp(TodoContext context, SignUpRequest signUpRequest)
        {
            _signUpRequestValidator.ValidateAndThrow(signUpRequest);

            var user = context.Users
                .SingleOrDefault(u => u.Email == signUpRequest.Email);
            if (user != null)
            {
                throw new TodoException(ServiceError.UserAlreadyRegistered);
            }

            var confirmationSecret = _secretGenerator.GenerateSecret();
            var pendingUser = new PendingUser
                {
                    Email = signUpRequest.Email,
                    Password = signUpRequest.Password,
                    Secret = confirmationSecret
                };
            context.PendingUsers.Add(pendingUser);
            context.SaveChanges();

            _signUpConfirmationNotifier.Notify(
                pendingUser.Email, 
                pendingUser.Secret);
        }
    }
}