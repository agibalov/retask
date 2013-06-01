using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class DeleteTaskRequestValidator : AbstractValidator<DeleteTaskRequest>
    {
        public DeleteTaskRequestValidator()
        {
            RuleFor(deleteTaskRequest => deleteTaskRequest.SessionToken).GoodSessionToken();
            RuleFor(deleteTaskRequest => deleteTaskRequest.TaskId).GoodId();
        }
    }
}