using System;
using System.Linq;
using FluentValidation;
using Ninject;
using Service.DAL;
using Service.DTO;
using Service.Exceptions;
using Service.TransactionScripts;
using Service.Validation.Requests;
using ResetPasswordRequest = Service.Validation.Requests.ResetPasswordRequest;

namespace Service
{
    public class RetaskService
    {
        [Inject] public SignInTransactionScript SignInTransactionScript { private get; set; }
        [Inject] public SignUpTransactionScript SignUpTransactionScript { private get; set; }
        [Inject] public ConfirmSignUpRequestTransactionScript ConfirmSignUpRequestTransactionScript { private get; set; }
        [Inject] public RequestPasswordResetTransactionScript RequestPasswordResetTransactionScript { private get; set; }
        [Inject] public ResetPasswordTransactionScript ResetPasswordTransactionScript { private get; set; }
        [Inject] public ChangePasswordTransactionScript ChangePasswordTransactionScript { private get; set; }
        [Inject] public PingTransactionScript PingTransactionScript { private get; set; }
        [Inject] public GetUserInfoTransactionScript GetUserInfoTransactionScript { private get; set; }
        [Inject] public GetDebugDataTransactionScript GetDebugDataTransactionScript { private get; set; }
        [Inject] public RebuildDatabaseTransactionScript RebuildDatabaseTransactionScript { private get; set; }
        [Inject] public CreateTaskTransactionScript CreateTaskTransactionScript { private get; set; }
        [Inject] public UpdateTaskTransactionScript UpdateTaskTransactionScript { private get; set; }
        [Inject] public ProgressTaskTransactionScript ProgressTaskTransactionScript { private get; set; }
        [Inject] public UnprogressTaskTransactionScript UnprogressTaskTransactionScript { private get; set; }
        [Inject] public GetWorkspaceTransactionScript GetWorkspaceTransactionScript { private get; set; }
        [Inject] public DeleteTaskTransactionScript DeleteTaskTransactionScript { private get; set; }

        public ServiceResult<SessionDTO> SignIn(string email, string password)
        {
            var signInRequest = new SignInRequest
                {
                    Email = SanitizeEmailAddress(email),
                    Password = password
                };

            return ExecuteWithExceptionHandling(
                context => SignInTransactionScript.SignIn(
                    context, 
                    signInRequest));
        }

        public ServiceResult<Object> SignUp(string email, string password)
        {
            var signUpRequest = new SignUpRequest
                {
                    Email = SanitizeEmailAddress(email),
                    Password = password
                };

            return ExecuteWithExceptionHandling(
                context => SignUpTransactionScript.SignUp(
                    context, 
                    signUpRequest));
        }

        public ServiceResult<Object> ConfirmSignUpRequest(string secret)
        {
            var signUpConfirmationRequest = new SignUpConfirmationRequest
                {
                    Secret = secret
                };

            return ExecuteWithExceptionHandling(
                context => ConfirmSignUpRequestTransactionScript.ConfirmSignUpRequest(
                    context,
                    signUpConfirmationRequest));
        }

        public ServiceResult<object> RequestPasswordReset(string email)
        {
            var requestPasswordResetRequest = new RequestPasswordResetRequest
                {
                    Email = SanitizeEmailAddress(email)
                };

            return ExecuteWithExceptionHandling(
                context => RequestPasswordResetTransactionScript.RequestPasswordReset(
                    context,
                    requestPasswordResetRequest));
        }

        public ServiceResult<object> ResetPassword(string secret, string password)
        {
            var resetPasswordRequest = new ResetPasswordRequest
                {
                    Secret = secret,
                    Password = password
                };

            return ExecuteWithExceptionHandling(
                context => ResetPasswordTransactionScript.ResetPassword(
                    context,
                    resetPasswordRequest));
        }

        public ServiceResult<object> ChangePassword(string sessionToken, string currentPassword, string newPassword)
        {
            var changePasswordRequest = new ChangePasswordRequest
                {
                    SessionToken = sessionToken,
                    CurrentPassword = currentPassword,
                    NewPassword = newPassword
                };

            return ExecuteWithExceptionHandling(
                context => ChangePasswordTransactionScript.ChangePassword(
                    context, 
                    changePasswordRequest));
        }

        public ServiceResult<object> Ping(string sessionToken)
        {
            var pingRequest = new PingRequest
            {
                SessionToken = sessionToken
            };

            return ExecuteWithExceptionHandling(
                context => PingTransactionScript.Ping(
                    context,
                    pingRequest));
        }

        public ServiceResult<UserInfoDTO> GetUserInfo(string sessionToken)
        {
            var getUserInfoRequest = new GetUserInfoRequest
                {
                    SessionToken = sessionToken
                };

            return ExecuteWithExceptionHandling(
                context => GetUserInfoTransactionScript.GetUserInfo(
                    context, 
                    getUserInfoRequest));
        }

        public ServiceResult<TaskDTO> CreateTask(string sessionToken, string taskDescription)
        {
            var createTaskRequest = new CreateTaskRequest
                {
                    SessionToken = sessionToken,
                    TaskDescription = taskDescription
                };

            return ExecuteWithExceptionHandling(
                context => CreateTaskTransactionScript.CreateTask(
                    context,
                    createTaskRequest));
        }

        public ServiceResult<TaskDTO> UpdateTask(string sessionToken, int taskId, string taskDescription)
        {
            var updateTaskRequest = new UpdateTaskRequest 
                { 
                    SessionToken = sessionToken, 
                    TaskId = taskId,
                    TaskDescription = taskDescription
                };

            return ExecuteWithExceptionHandling(
                context => UpdateTaskTransactionScript.UpdateTask(
                    context,
                    updateTaskRequest));
        }

        public ServiceResult<TaskDTO> ProgressTask(string sessionToken, int taskId)
        {
            var progressTaskRequest = new ProgressTaskRequest
                {
                    SessionToken = sessionToken,
                    TaskId = taskId
                };

            return ExecuteWithExceptionHandling(
                context => ProgressTaskTransactionScript.ProgressTask(
                    context,
                    progressTaskRequest));
        }

        public ServiceResult<TaskDTO> UnprogressTask(string sessionToken, int taskId)
        {
            var unprogressTaskRequest = new UnprogressTaskRequest
                {
                    SessionToken = sessionToken,
                    TaskId = taskId
                };

            return ExecuteWithExceptionHandling(
                context => UnprogressTaskTransactionScript.UnprogressTask(
                    context, 
                    unprogressTaskRequest));
        }

        public ServiceResult<object> DeleteTask(string sessionToken, int taskId)
        {
            var deleteTaskRequest = new DeleteTaskRequest
                {
                    SessionToken = sessionToken,
                    TaskId = taskId
                };

            return ExecuteWithExceptionHandling(
                context => DeleteTaskTransactionScript.DeleteTask(
                    context, 
                    deleteTaskRequest));
        }

        public ServiceResult<WorkspaceDTO> GetWorkspace(string sessionToken)
        {
            var getWorkspaceRequest = new GetWorkspaceRequest
                {
                    SessionToken = sessionToken
                };

            return ExecuteWithExceptionHandling(
                context => GetWorkspaceTransactionScript.GetWorkspace(
                    context, 
                    getWorkspaceRequest));
        }

        public ServiceResult<DebugDTO> GetDebugData()
        {
            return ExecuteWithExceptionHandling(
                context => GetDebugDataTransactionScript.GetDebugData(
                    context));
        }

        public ServiceResult<Object> RebuildDatabase()
        {
            return ExecuteWithExceptionHandling(
                context => RebuildDatabaseTransactionScript.RebuildDatabase(
                    context));
        }

        private static ServiceResult<object> ExecuteWithExceptionHandling(Action<TodoContext> action)
        {
            return ExecuteWithExceptionHandling<object>(context =>
                {
                    action(context);
                    return null;
                });
        }

        private static ServiceResult<T> ExecuteWithExceptionHandling<T>(Func<TodoContext, T> func)
        {
            try
            {
                using (var context = new TodoContext())
                {
                    var result = func(context);
                    context.SaveChanges();
                    return new ServiceResult<T>
                        {
                            Ok = true,
                            Payload = result
                        };
                }
            }
            catch (ValidationException e)
            {
                var failures = e.Errors;
                var errorGroups = failures
                    .GroupBy(failure => failure.PropertyName)
                    .ToDictionary(
                        errorGroup => errorGroup.Key, 
                        errorGroup => errorGroup.Select(g => g.ErrorMessage).ToList());

                return new ServiceResult<T>
                    {
                        Ok = false,
                        Error = ServiceError.ValidationError,
                        FieldsInError = errorGroups
                    };
            }
            catch (TodoException e)
            {
                return new ServiceResult<T>
                    {
                        Ok = false,
                        Error = e.ServiceError
                    };
            }
            catch (Exception)
            {
                return new ServiceResult<T>
                    {
                        Ok = false,
                        Error = ServiceError.InternalError
                    };
            }
        }

        private static string SanitizeEmailAddress(string emailString)
        {
            if (emailString == null)
            {
                return null;
            }

            return emailString.Trim().ToLower();
        }
    }
}
