using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class UnprogressTaskRequestValidator : AbstractValidator<UnprogressTaskRequest>
    {
        public UnprogressTaskRequestValidator()
        {
            RuleFor(unprogressTaskRequest => unprogressTaskRequest.SessionToken).GoodSessionToken();
            RuleFor(unprogressTaskRequest => unprogressTaskRequest.TaskId).GoodId();
        }
    }
}