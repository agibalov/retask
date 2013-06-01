using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidator()
        {
            RuleFor(signInRequest => signInRequest.Email).GoodEmail();
            RuleFor(signInRequest => signInRequest.Password).GoodPassword();
        }
    }
}