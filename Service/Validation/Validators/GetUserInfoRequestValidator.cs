using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class GetUserInfoRequestValidator : AbstractValidator<GetUserInfoRequest>
    {
        public GetUserInfoRequestValidator()
        {
            RuleFor(getUserInfoRequest => getUserInfoRequest.SessionToken).GoodSessionToken();
        }
    }
}