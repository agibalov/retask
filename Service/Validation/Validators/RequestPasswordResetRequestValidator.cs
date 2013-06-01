using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class RequestPasswordResetRequestValidator : AbstractValidator<RequestPasswordResetRequest>
    {
        public RequestPasswordResetRequestValidator()
        {
            RuleFor(requestPasswordResetRequest => requestPasswordResetRequest.Email).GoodEmail();
        }
    }
}