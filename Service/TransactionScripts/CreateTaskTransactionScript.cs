using FluentValidation;
using Service.DAL;
using Service.DTO;
using Service.Mappers;
using Service.TransactionScripts.BL;
using Service.Validation.Requests;
using Service.Validation.Validators;

namespace Service.TransactionScripts
{
    public class CreateTaskTransactionScript
    {
        private readonly AuthenticationService _authenticationService;
        private readonly TaskToTaskDTOMapper _taskToTaskDtoMapper;
        private readonly CreateTaskRequestValidator _createTaskRequestValidator;
        private readonly ITimeProvider _timeProvider;

        public CreateTaskTransactionScript(
            AuthenticationService authenticationService,
            TaskToTaskDTOMapper taskToTaskDtoMapper,
            CreateTaskRequestValidator createTaskRequestValidator,
            ITimeProvider timeProvider)
        {
            _authenticationService = authenticationService;
            _taskToTaskDtoMapper = taskToTaskDtoMapper;
            _createTaskRequestValidator = createTaskRequestValidator;
            _timeProvider = timeProvider;
        }

        public TaskDTO CreateTask(TodoContext context, CreateTaskRequest createTaskRequest)
        {
            _createTaskRequestValidator.ValidateAndThrow(createTaskRequest);

            var user = _authenticationService.GetUserBySessionToken(
                context, 
                createTaskRequest.SessionToken);

            var task = new Task
                {
                    TaskStatus = (int)TaskStatus.NotStarted,
                    TaskDescription = createTaskRequest.TaskDescription,
                    CreatedAt = _timeProvider.GetCurrentTime(),
                    ModifiedAt = null,
                    User = user
                };
            context.Tasks.Add(task);
            context.SaveChanges();
            
            return _taskToTaskDtoMapper.Map(task);
        }
    }
}