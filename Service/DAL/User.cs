using System;
using System.Collections.Generic;

namespace Service.DAL
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public IList<Session> Sessions { get; set; }
        public IList<Task> Tasks { get; set; }
        public IList<PendingPasswordReset> PendingPasswordResets { get; set; }
        public int AuthenticationCount { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? LastActivityAt { get; set; }
    }
}