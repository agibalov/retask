using FluentValidation;
using Service.DAL;
using Service.TransactionScripts.BL;
using Service.Validation.Requests;
using Service.Validation.Validators;

namespace Service.TransactionScripts
{
    public class PingTransactionScript
    {
        private readonly AuthenticationService _authenticationService;
        private readonly PingRequestValidator _pingRequestValidator;

        public PingTransactionScript(
            AuthenticationService authenticationService,
            PingRequestValidator pingRequestValidator)
        {
            _authenticationService = authenticationService;
            _pingRequestValidator = pingRequestValidator;
        }

        public void Ping(TodoContext context, PingRequest pingRequest)
        {
            _pingRequestValidator.ValidateAndThrow(pingRequest);

            _authenticationService.MakeSureSessionIsValid(
                context, 
                pingRequest.SessionToken);
        }
    }
}