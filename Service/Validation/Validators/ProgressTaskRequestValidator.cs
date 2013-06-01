using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class ProgressTaskRequestValidator : AbstractValidator<ProgressTaskRequest>
    {
        public ProgressTaskRequestValidator()
        {
            RuleFor(progressTaskRequest => progressTaskRequest.SessionToken).GoodSessionToken();
            RuleFor(progressTaskRequest => progressTaskRequest.TaskId).GoodId();
        }
    }
}