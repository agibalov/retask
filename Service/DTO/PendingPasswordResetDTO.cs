namespace Service.DTO
{
    public class PendingPasswordResetDTO
    {
        public int PendingPasswordResetId { get; set; }
        public int UserId { get; set; }
        public string Secret { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}