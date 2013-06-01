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
    public class UnprogressTaskTransactionScript
    {
        private readonly AuthenticationService _authenticationService;
        private readonly TaskToTaskDTOMapper _taskToTaskDtoMapper;
        private readonly TaskProgressLogic _taskProgressLogic;
        private readonly UnprogressTaskRequestValidator _unprogressTaskRequestValidator;
        private readonly ITimeProvider _timeProvider;

        public UnprogressTaskTransactionScript(
            AuthenticationService authenticationService,
            TaskToTaskDTOMapper taskToTaskDtoMapper,
            TaskProgressLogic taskProgressLogic,
            UnprogressTaskRequestValidator unprogressTaskRequestValidator,
            ITimeProvider timeProvider)
        {
            _authenticationService = authenticationService;
            _taskToTaskDtoMapper = taskToTaskDtoMapper;
            _taskProgressLogic = taskProgressLogic;
            _unprogressTaskRequestValidator = unprogressTaskRequestValidator;
            _timeProvider = timeProvider;
        }

        public TaskDTO UnprogressTask(TodoContext context, UnprogressTaskRequest unprogressTaskRequest)
        {
            _unprogressTaskRequestValidator.ValidateAndThrow(unprogressTaskRequest);

            var user = _authenticationService.GetUserBySessionToken(
                context, 
                unprogressTaskRequest.SessionToken);

            var task = context.Tasks
                .Include("User")
                .SingleOrDefault(t => t.TaskId == unprogressTaskRequest.TaskId);
            if (task == null)
            {
                throw new TodoException(ServiceError.NoSuchTask);
            }

            if (task.UserId != user.UserId)
            {
                throw new TodoException(ServiceError.NoPermissions);
            }

            var newTaskStatus = _taskProgressLogic.GetPreviousFor((TaskStatus)task.TaskStatus);
            task.TaskStatus = (int)newTaskStatus;
            task.ModifiedAt = _timeProvider.GetCurrentTime();
            context.SaveChanges();

            return _taskToTaskDtoMapper.Map(task);
        }
    }
}