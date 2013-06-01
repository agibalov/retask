using System.ComponentModel;
using System.Web.Http;
using Service;
using Service.DTO;

namespace WebApp.Controllers
{
    public class RetaskApiController : ApiController
    {
        private readonly RetaskService _retaskService;

        public RetaskApiController(RetaskService retaskService)
        {
            _retaskService = retaskService;
        }

        [Description("Authenticate registered user with email and password.")]
        [HttpPost]
        [ActionName("SignIn")]
        public ServiceResult<SessionDTO> SignIn(
            [Description("Email")] string email,
            [Description("Password")] string password)
        {
            return _retaskService.SignIn(email, password);
        }

        [Description("Accept new user's sign up request. This sends a sign up confirmation email.")] 
        [HttpPost]
        [ActionName("SignUp")]
        public ServiceResult<object> SignUp(
            [Description("Email")] string email,
            [Description("Password")] string password)
        {
            return _retaskService.SignUp(email, password);
        }

        [Description("Return user Id and Email for authenticated user.")] 
        [HttpGet]
        [ActionName("GetUserInfo")]
        public ServiceResult<UserInfoDTO> GetUserInfo(
            [Description("Session Token")] string sessionToken)
        {
            return _retaskService.GetUserInfo(sessionToken);
        }

        [Description("Keep session alive.")]
        [HttpPost]
        [ActionName("Ping")]
        public ServiceResult<object> Ping(
            [Description("Session Token")] string sessionToken)
        {
            return _retaskService.Ping(sessionToken);
        }
        
        [Description("Create a new task with default status of \"Not Started\".")] 
        [HttpPost]
        [ActionName("CreateTask")]
        public ServiceResult<TaskDTO> CreateTask(
            [Description("Session Token")] string sessionToken,
            [Description("Task description")] TaskDescriptionModel taskDescriptionModel)
        {
            return _retaskService.CreateTask(
                sessionToken, 
                taskDescriptionModel.TaskDescription);
        }

        [Description("Update task description.")] 
        [HttpPost]
        [ActionName("UpdateTask")]
        public ServiceResult<TaskDTO> UpdateTask(
            [Description("Session Token")] string sessionToken,
            [Description("Task Id")] int taskId,
            [Description("New task description")] TaskDescriptionModel taskDescriptionModel)
        {
            return _retaskService.UpdateTask(
                sessionToken, 
                taskId, 
                taskDescriptionModel.TaskDescription);
        }

        [Description("Change task status - progress it.")] 
        [HttpPost]
        [ActionName("ProgressTask")]
        public ServiceResult<TaskDTO> ProgressTask(
            [Description("Session Token")] string sessionToken,
            [Description("Task Id")] int taskId)
        {
            return _retaskService.ProgressTask(
                sessionToken, 
                taskId);
        }

        [Description("Change task status - unprogress it.")] 
        [HttpPost]
        [ActionName("UnprogressTask")]
        public ServiceResult<TaskDTO> UnprogressTask(
            [Description("Session Token")] string sessionToken,
            [Description("Task Id")] int taskId)
        {
            return _retaskService.UnprogressTask(
                sessionToken, 
                taskId);
        }

        [Description("Delete task.")] 
        [HttpPost]
        [ActionName("DeleteTask")]
        public ServiceResult<object> DeleteTask(
            [Description("Session Token")] string sessionToken,
            [Description("Task Id")] int taskId)
        {
            return _retaskService.DeleteTask(
                sessionToken, 
                taskId);
        }

        [Description("Get user's active tasks.")] 
        [HttpGet]
        [ActionName("GetWorkspace")]
        public ServiceResult<WorkspaceDTO> GetWorkspace(
            [Description("Session Token")] string sessionToken)
        {
            return _retaskService.GetWorkspace(
                sessionToken);
        }
    }

    public class TaskDescriptionModel
    {
        public string TaskDescription { get; set; }
    }
}