namespace Service.Validation.Requests
{
    public class ProgressTaskRequest
    {
        public string SessionToken { get; set; }
        public int TaskId { get; set; }
    }
}