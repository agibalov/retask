using Service.DAL;
using Service.Exceptions;

namespace Service.TransactionScripts.BL
{
    public class TaskProgressLogic
    {
        public TaskStatus GetNextFor(TaskStatus taskStatus)
        {
            if (taskStatus == TaskStatus.NotStarted)
            {
                return TaskStatus.InProgress;
            }
            
            if (taskStatus == TaskStatus.InProgress)
            {
                return TaskStatus.Done;
            }

            if (taskStatus == TaskStatus.Done)
            {
                return TaskStatus.Complete;
            }

            throw new TodoException(ServiceError.InvalidTaskStatus);
        }

        public TaskStatus GetPreviousFor(TaskStatus taskStatus)
        {
            if (taskStatus == TaskStatus.InProgress)
            {
                return TaskStatus.NotStarted;
            }

            if (taskStatus == TaskStatus.Done)
            {
                return TaskStatus.InProgress;
            }

            throw new TodoException(ServiceError.InvalidTaskStatus);
        }
    }
}