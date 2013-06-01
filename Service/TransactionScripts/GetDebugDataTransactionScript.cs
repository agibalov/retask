using System.Linq;
using Service.DAL;
using Service.DTO;

namespace Service.TransactionScripts
{
    public class GetDebugDataTransactionScript
    {
        public DebugDTO GetDebugData(TodoContext context)
        {
            var users = (from user in context.Users
                         let taskCount = context.Tasks.Count(t => t.UserId == user.UserId)
                         let notStartedTaskCount = context.Tasks.Count(t => t.UserId == user.UserId && t.TaskStatus == (int)TaskStatus.NotStarted)
                         let inProgressTaskCount = context.Tasks.Count(t => t.UserId == user.UserId && t.TaskStatus == (int)TaskStatus.InProgress)
                         let doneTaskCount = context.Tasks.Count(t => t.UserId == user.UserId && t.TaskStatus == (int)TaskStatus.Done)
                         let completeTaskCount = context.Tasks.Count(t => t.UserId == user.UserId && t.TaskStatus == (int)TaskStatus.Complete)
                         select new UserDTO
                             {
                                 UserId = user.UserId,
                                 Email = user.Email,
                                 AuthenticationCount = user.AuthenticationCount,
                                 RegisteredAt = user.RegisteredAt,
                                 LastActivityAt = user.LastActivityAt,
                                 TaskCount = taskCount,
                                 NotStartedTaskCount = notStartedTaskCount,
                                 InProgressTaskCount = inProgressTaskCount,
                                 DoneTaskCount = doneTaskCount,
                                 CompleteTaskCount = completeTaskCount
                             }).ToList();

            var pendingUsers = context.PendingUsers
                .Select(pu => new PendingUserDTO
                    {
                        PendingUserId = pu.PendingUserId,
                        Email = pu.Email,
                        Password = pu.Password,
                        Secret = pu.Secret
                    }).ToList();

            var pendingPasswordResets = 
                (from pendingPasswordReset in context.PendingPasswordResets
                 join user in context.Users on pendingPasswordReset.UserId equals user.UserId
                 select new PendingPasswordResetDTO
                    {
                        PendingPasswordResetId = pendingPasswordReset.PendingPasswordResetId,
                        UserId = pendingPasswordReset.UserId,
                        Secret = pendingPasswordReset.Secret,
                        Email = user.Email,
                        Password = user.Password
                    }).ToList();

            return new DebugDTO
                {
                    Users = users,
                    PendingUsers = pendingUsers,
                    PendingPasswordResets = pendingPasswordResets
                };
        }
    }
}