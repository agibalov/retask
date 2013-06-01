using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(changePasswordRequest => changePasswordRequest.SessionToken).GoodSessionToken();
            RuleFor(changePasswordRequest => changePasswordRequest.CurrentPassword).GoodPassword();
            RuleFor(changePasswordRequest => changePasswordRequest.NewPassword).GoodPassword();
        }
    }
}