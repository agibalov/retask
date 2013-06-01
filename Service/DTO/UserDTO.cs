using System;

namespace Service.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int AuthenticationCount { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? LastActivityAt { get; set; }
        public int TaskCount { get; set; }
        public int NotStartedTaskCount { get; set; }
        public int InProgressTaskCount { get; set; }
        public int DoneTaskCount { get; set; }
        public int CompleteTaskCount { get; set; }
    }
}