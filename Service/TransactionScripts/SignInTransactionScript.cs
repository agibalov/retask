using System;
using System.Linq;
using FluentValidation;
using Service.DAL;
using Service.DTO;
using Service.Exceptions;
using Service.TransactionScripts.BL;
using Service.Validation.Requests;
using Service.Validation.Validators;

namespace Service.TransactionScripts
{
    public class SignInTransactionScript
    {
        private readonly SignInRequestValidator _signInRequestValidator;
        private readonly SecretGenerator _secretGenerator;
        private readonly ITimeProvider _timeProvider;

        public SignInTransactionScript(
            SignInRequestValidator signInRequestValidator,
            SecretGenerator secretGenerator,
            ITimeProvider timeProvider)
        {
            _signInRequestValidator = signInRequestValidator;
            _secretGenerator = secretGenerator;
            _timeProvider = timeProvider;
        }

        public SessionDTO SignIn(TodoContext context, SignInRequest signInRequest)
        {
            _signInRequestValidator.ValidateAndThrow(signInRequest);

            var user = context.Users.SingleOrDefault(u => u.Email == signInRequest.Email);
            if (user == null)
            {
                throw new TodoException(ServiceError.NoSuchUser);
            }

            if (user.Password != signInRequest.Password)
            {
                throw new TodoException(ServiceError.InvalidPassword);
            }

            ++user.AuthenticationCount;
            user.LastActivityAt = _timeProvider.GetCurrentTime();

            var session = new Session
                {
                    Token = _secretGenerator.GenerateSecret(),
                    User = user,
                    LastAccessTime = _timeProvider.GetCurrentTime()
                };
            context.Sessions.Add(session);
            context.SaveChanges();

            return new SessionDTO
                {
                    SessionToken = session.Token
                };
        }
    }
}