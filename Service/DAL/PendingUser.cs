namespace Service.DAL
{
    public class PendingUser
    {
        public int PendingUserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Secret { get; set; }
    }
}