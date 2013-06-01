using FluentValidation;
using Service.Validation.Requests;

namespace Service.Validation.Validators
{
    public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidator()
        {
            RuleFor(createTaskRequest => createTaskRequest.SessionToken).GoodSessionToken();
            RuleFor(createTaskRequest => createTaskRequest.TaskDescription).GoodTaskDescription();
        }
    }
}