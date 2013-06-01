using NUnit.Framework;
using Service.DAL;
using Service.DTO;

namespace ServiceTests.Extensions
{
    public static class TaskDTOAssertExtensions
    {
        public static TaskDTO Ok(this ServiceResult<TaskDTO> result)
        {
            var task = result.Ok<TaskDTO>();
            Assert.IsNotNull(task);
            return task;
        }

        public static TaskDTO HasTaskId(this TaskDTO task)
        {
            task.TaskId.AssertAdequateId("TaskId should not be 0");
            return task;
        }

        public static TaskDTO HasTaskId(this TaskDTO task, int taskId)
        {
            Assert.AreEqual(taskId, task.TaskId);
            return task;
        }

        public static TaskDTO HasTaskDescription(this TaskDTO task)
        {
            task.TaskDescription.AssertNotEmptyString("TaskDescription should not be empty");
            return task;
        }

        public static TaskDTO HasTaskDescription(this TaskDTO task, string taskDescription)
        {
            Assert.AreEqual(taskDescription, task.TaskDescription);
            return task;
        }

        public static TaskDTO HasTaskStatus(this TaskDTO task, TaskStatus taskStatus)
        {
            Assert.AreEqual(taskStatus, task.TaskStatus);
            return task;
        }

        public static TaskDTO SameAs(this TaskDTO task, TaskDTO otherTask)
        {
            Assert.AreEqual(task.TaskId, otherTask.TaskId);
            Assert.AreEqual(task.TaskDescription, otherTask.TaskDescription);
            Assert.AreEqual(task.TaskStatus, otherTask.TaskStatus);
            Assert.AreEqual(task.CreatedAt, otherTask.CreatedAt);
            Assert.AreEqual(task.ModifiedAt, otherTask.ModifiedAt);
            return task;
        }

        public static bool BoolSameAs(this TaskDTO task, TaskDTO otherTask)
        {
            if (task.TaskId != otherTask.TaskId)
            {
                return false;
            }

            if (task.TaskDescription != otherTask.TaskDescription)
            {
                return false;
            }

            if (task.TaskStatus != otherTask.TaskStatus)
            {
                return false;
            }
            
            if (task.ModifiedAt.HasValue != otherTask.ModifiedAt.HasValue)
            {
                return false;
            }

            return true;
        }
    }
}