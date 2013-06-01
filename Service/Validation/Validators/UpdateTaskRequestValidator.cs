using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskRequestValidator()
        {
            RuleFor(updateTaskRequest => updateTaskRequest.SessionToken).GoodSessionToken();
            RuleFor(updateTaskRequest => updateTaskRequest.TaskId).GoodId();
            RuleFor(updateTaskRequest => updateTaskRequest.TaskDescription).GoodTaskDescription();
        }
    }
}