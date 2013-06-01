using System;
using Service.DAL;

namespace Service.DTO
{
    public class TaskDTO
    {
        public int TaskId { get; set; }
        public string TaskDescription { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}