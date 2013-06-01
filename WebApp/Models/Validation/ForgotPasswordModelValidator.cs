using FluentValidation;
using Service.Validation;

namespace WebApp.Models.Validation
{
    public class ForgotPasswordModelValidator : AbstractValidator<ForgotPasswordModel>
    {
        public ForgotPasswordModelValidator()
        {
            RuleFor(forgotPasswordModel => forgotPasswordModel.Email).GoodEmail();
        }
    }
}