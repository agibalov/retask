using FluentValidation;
using Service.Validation;

namespace WebApp.Models.Validation
{
    public class SignInModelValidator : AbstractValidator<SignInModel>
    {
        public SignInModelValidator()
        {
            RuleFor(signInModel => signInModel.Email).GoodEmail();
            RuleFor(signInModel => signInModel.Password).GoodPassword();
        }
    }
}