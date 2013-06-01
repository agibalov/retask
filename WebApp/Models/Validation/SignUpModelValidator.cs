using FluentValidation;
using Service.Validation;

namespace WebApp.Models.Validation
{
    public class SignUpModelValidator : AbstractValidator<SignUpModel>
    {
        public SignUpModelValidator()
        {
            RuleFor(signUpModel => signUpModel.Email).GoodEmail();
            RuleFor(signUpModel => signUpModel.Password).GoodPassword();
        }
    }
}