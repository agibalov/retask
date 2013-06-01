namespace Service.Validation.Requests
{
    public class ResetPasswordRequest
    {
        public string Secret { get; set; }
        public string Password { get; set; }
    }
}