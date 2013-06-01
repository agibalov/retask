using System;

namespace Service.DAL
{
    public class Session
    {
        public int SessionId { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime LastAccessTime { get; set; }
    }
}