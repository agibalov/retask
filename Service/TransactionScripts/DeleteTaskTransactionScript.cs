using System.Linq;
using FluentValidation;
using Service.DAL;
using Service.Exceptions;
using Service.TransactionScripts.BL;
using Service.Validation.Requests;
using Service.Validation.Validators;

namespace Service.TransactionScripts
{
    public class DeleteTaskTransactionScript
    {
        private readonly AuthenticationService _authenticationService;
        private readonly DeleteTaskRequestValidator _deleteTaskRequestValidator;

        public DeleteTaskTransactionScript(
            AuthenticationService authenticationService,
            DeleteTaskRequestValidator deleteTaskRequestValidator)
        {
            _authenticationService = authenticationService;
            _deleteTaskRequestValidator = deleteTaskRequestValidator;
        }

        public void DeleteTask(TodoContext context, DeleteTaskRequest deleteTaskRequest)
        {
            _deleteTaskRequestValidator.ValidateAndThrow(deleteTaskRequest);

            var user = _authenticationService.GetUserBySessionToken(
                context, 
                deleteTaskRequest.SessionToken);

            var task = context.Tasks
                .Include("User")
                .SingleOrDefault(t => t.TaskId == deleteTaskRequest.TaskId);
            if (task == null)
            {
                throw new TodoException(ServiceError.NoSuchTask);
            }

            if (task.UserId != user.UserId)
            {
                throw new TodoException(ServiceError.NoPermissions);
            }

            context.Tasks.Remove(task);
            context.SaveChanges();
        }
    }
}