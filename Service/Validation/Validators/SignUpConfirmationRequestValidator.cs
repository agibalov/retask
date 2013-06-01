using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class SignUpConfirmationRequestValidator : AbstractValidator<SignUpConfirmationRequest>
    {
        public SignUpConfirmationRequestValidator()
        {
            RuleFor(signUpConfirmationRequest => signUpConfirmationRequest.Secret).NotEmpty();
        }
    }
}