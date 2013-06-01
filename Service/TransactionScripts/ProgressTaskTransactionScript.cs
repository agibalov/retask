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
    public class ProgressTaskTransactionScript
    {
        private readonly AuthenticationService _authenticationService;
        private readonly TaskToTaskDTOMapper _taskToTaskDtoMapper;
        private readonly TaskProgressLogic _taskProgressLogic;
        private readonly ProgressTaskRequestValidator _progressTaskRequestValidator;
        private readonly ITimeProvider _timeProvider;

        public ProgressTaskTransactionScript(
            AuthenticationService authenticationService,
            TaskToTaskDTOMapper taskToTaskDtoMapper,
            TaskProgressLogic taskProgressLogic,
            ProgressTaskRequestValidator progressTaskRequestValidator,
            ITimeProvider timeProvider)
        {
            _authenticationService = authenticationService;
            _taskToTaskDtoMapper = taskToTaskDtoMapper;
            _taskProgressLogic = taskProgressLogic;
            _progressTaskRequestValidator = progressTaskRequestValidator;
            _timeProvider = timeProvider;
        }

        public TaskDTO ProgressTask(TodoContext context, ProgressTaskRequest progressTaskRequest)
        {
            _progressTaskRequestValidator.ValidateAndThrow(progressTaskRequest);

            var user = _authenticationService.GetUserBySessionToken(
                context, 
                progressTaskRequest.SessionToken);

            var task = context.Tasks
                .Include("User")
                .SingleOrDefault(t => t.TaskId == progressTaskRequest.TaskId);
            if (task == null)
            {
                throw new TodoException(ServiceError.NoSuchTask);
            }

            if (task.UserId != user.UserId)
            {
                throw new TodoException(ServiceError.NoPermissions);
            }

            var newTaskStatus = _taskProgressLogic.GetNextFor((TaskStatus)task.TaskStatus);
            task.TaskStatus = (int)newTaskStatus;
            task.ModifiedAt = _timeProvider.GetCurrentTime();
            context.SaveChanges();

            return _taskToTaskDtoMapper.Map(task);
        }
    }
}