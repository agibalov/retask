namespace Service.DAL
{
    public class PendingPasswordReset
    {
        public int PendingPasswordResetId { get; set; }
        public string Secret { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}