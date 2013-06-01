using System;

namespace Service.DAL
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskDescription { get; set; }
        public int TaskStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}