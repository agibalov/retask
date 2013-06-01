using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class GetWorkspaceRequestValidator : AbstractValidator<GetWorkspaceRequest>
    {
        public GetWorkspaceRequestValidator()
        {
            RuleFor(getWorkspaceRequest => getWorkspaceRequest.SessionToken).GoodSessionToken();
        }
    }
}