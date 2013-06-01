namespace Service.Validation.Requests
{
    public class ChangePasswordRequest
    {
        public string SessionToken { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}