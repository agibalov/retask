using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class PingRequestValidator : AbstractValidator<PingRequest>
    {
        public PingRequestValidator()
        {
            RuleFor(pingRequest => pingRequest.SessionToken).GoodSessionToken();
        }
    }
}