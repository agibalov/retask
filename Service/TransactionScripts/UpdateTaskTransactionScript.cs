using System;
using System.Linq;
using FluentValidation;
using Service.DAL;
using Service.DTO;
using Service.Exceptions;
using Service.Mappers;
using Service.TransactionScripts.BL;
using Service.Validation.Requests;
using Service.Validation.Validators;

namespace Service.TransactionScripts
{
    public class UpdateTaskTransactionScript
    {
        private readonly AuthenticationService _authenticationService;
        private readonly TaskToTaskDTOMapper _taskToTaskDtoMapper;
        private readonly UpdateTaskRequestValidator _updateTaskRequestValidator;
        private readonly ITimeProvider _timeProvider;

        public UpdateTaskTransactionScript(
            AuthenticationService authenticationService,
            TaskToTaskDTOMapper taskToTaskDtoMapper,
            UpdateTaskRequestValidator updateTaskRequestValidator,
            ITimeProvider timeProvider)
        {
            _authenticationService = authenticationService;
            _taskToTaskDtoMapper = taskToTaskDtoMapper;
            _updateTaskRequestValidator = updateTaskRequestValidator;
            _timeProvider = timeProvider;
        }

        public TaskDTO UpdateTask(TodoContext context, UpdateTaskRequest updateTaskRequest)
        {
            _updateTaskRequestValidator.ValidateAndThrow(updateTaskRequest);

            var user = _authenticationService.GetUserBySessionToken(
                context, 
                updateTaskRequest.SessionToken);

            var task = context.Tasks
                .Include("User")
                .SingleOrDefault(t => t.TaskId == updateTaskRequest.TaskId);
            if (task == null)
            {
                throw new TodoException(ServiceError.NoSuchTask);
            }

            if (task.UserId != user.UserId)
            {
                throw new TodoException(ServiceError.NoPermissions);
            }

            task.TaskDescription = updateTaskRequest.TaskDescription;
            task.ModifiedAt = _timeProvider.GetCurrentTime();
            context.SaveChanges();

            return _taskToTaskDtoMapper.Map(task);
        }
    }
}