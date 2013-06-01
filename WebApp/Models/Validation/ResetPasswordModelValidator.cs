using FluentValidation;
using Service.Validation;

namespace WebApp.Models.Validation
{
    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
    {
        public ResetPasswordModelValidator()
        {
            RuleFor(resetPasswordModel => resetPasswordModel.Secret).GoodResetPasswordSecret();
            RuleFor(resetPasswordModel => resetPasswordModel.Password).GoodPassword();
        }
    }
}