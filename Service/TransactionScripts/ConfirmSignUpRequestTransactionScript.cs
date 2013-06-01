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
    public class ConfirmSignUpRequestTransactionScript
    {
        private readonly SignUpConfirmationRequestValidator _signUpConfirmationRequestValidator;
        private readonly IWelcomeNotifier _welcomeNotifier;
        private readonly ITimeProvider _timeProvider;

        public ConfirmSignUpRequestTransactionScript(
            SignUpConfirmationRequestValidator signUpConfirmationRequestValidator,
            IWelcomeNotifier welcomeNotifier,
            ITimeProvider timeProvider)
        {
            _signUpConfirmationRequestValidator = signUpConfirmationRequestValidator;
            _welcomeNotifier = welcomeNotifier;
            _timeProvider = timeProvider;
        }

        public void ConfirmSignUpRequest(TodoContext context, SignUpConfirmationRequest signUpConfirmationRequest)
        {
            _signUpConfirmationRequestValidator.ValidateAndThrow(signUpConfirmationRequest);

            var pendingUser = context.PendingUsers
                .SingleOrDefault(pu => pu.Secret == signUpConfirmationRequest.Secret);
            if (pendingUser == null)
            {
                throw new TodoException(ServiceError.NoSuchPendingUser);
            }

            var existingUser = context.Users
                .SingleOrDefault(u => u.Email == pendingUser.Email);
            if (existingUser != null)
            {
                throw new TodoException(ServiceError.UserAlreadyRegistered);
            }

            var user = new User
                {
                    Email = pendingUser.Email,
                    Password = pendingUser.Password,
                    IsAdmin = false,
                    AuthenticationCount = 0,
                    RegisteredAt = _timeProvider.GetCurrentTime()
                };
            context.Users.Add(user);
            context.PendingUsers.Remove(pendingUser);
            context.SaveChanges();

            _welcomeNotifier.Notify(user.Email);
        }
    }
}