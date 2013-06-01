using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(resetPasswordRequest => resetPasswordRequest.Secret).GoodResetPasswordSecret();
            RuleFor(resetPasswordRequest => resetPasswordRequest.Password).GoodPassword();
        }
    }
}