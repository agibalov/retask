using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidator()
        {
            RuleFor(signUpRequest => signUpRequest.Email).GoodEmail();
            RuleFor(signUpRequest => signUpRequest.Password).GoodPassword();
        }
    }
}