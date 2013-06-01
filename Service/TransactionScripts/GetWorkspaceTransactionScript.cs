using System;
using System.Linq;
using FluentValidation;
using Service.DAL;
using Service.DTO;
using Service.Mappers;
using Service.TransactionScripts.BL;
using Service.Validation.Requests;
using Service.Validation.Validators;

namespace Service.TransactionScripts
{
    public class GetWorkspaceTransactionScript
    {
        private readonly AuthenticationService _authenticationService;
        private readonly TaskToTaskDTOMapper _taskToTaskDtoMapper;
        private readonly GetWorkspaceRequestValidator _getWorkspaceRequestValidator;

        public GetWorkspaceTransactionScript(
            AuthenticationService authenticationService,
            TaskToTaskDTOMapper taskToTaskDtoMapper,
            GetWorkspaceRequestValidator getWorkspaceRequestValidator)
        {
            _authenticationService = authenticationService;
            _taskToTaskDtoMapper = taskToTaskDtoMapper;
            _getWorkspaceRequestValidator = getWorkspaceRequestValidator;
        }

        public WorkspaceDTO GetWorkspace(TodoContext context, GetWorkspaceRequest getWorkspaceRequest)
        {
            _getWorkspaceRequestValidator.ValidateAndThrow(getWorkspaceRequest);

            var user = _authenticationService.GetUserBySessionToken(
                context, 
                getWorkspaceRequest.SessionToken);

            Func<Task, bool> taskIsOwnedByUserAndIsNotComplete = task => 
                task.UserId == user.UserId &&
                task.TaskStatus != (int) TaskStatus.Complete;

            var tasks = context.Tasks.Where(taskIsOwnedByUserAndIsNotComplete).ToList();
            return new WorkspaceDTO
                {
                    Tasks = _taskToTaskDtoMapper.Map(tasks)
                };
        }
    }
}