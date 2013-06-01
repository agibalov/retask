namespace Service.Validation.Requests
{
    public class CreateTaskRequest
    {
        public string SessionToken { get; set; }
        public string TaskDescription { get; set; }
    }
}