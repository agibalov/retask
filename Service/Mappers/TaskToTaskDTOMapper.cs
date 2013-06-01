using System.Collections.Generic;
using System.Linq;
using Service.DAL;
using Service.DTO;

namespace Service.Mappers
{
    public class TaskToTaskDTOMapper
    {
        public TaskDTO Map(Task task)
        {
            return new TaskDTO
                {
                    TaskId = task.TaskId,
                    TaskDescription = task.TaskDescription,
                    TaskStatus = (TaskStatus)task.TaskStatus,
                    CreatedAt = task.CreatedAt,
                    ModifiedAt = task.ModifiedAt
                };
        }

        public IList<TaskDTO> Map(IEnumerable<Task> tasks)
        {
            return tasks.Select(Map).ToList();
        }
    }
}