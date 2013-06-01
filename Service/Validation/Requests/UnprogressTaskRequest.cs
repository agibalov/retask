namespace Service.Validation.Requests
{
    public class UnprogressTaskRequest
    {
        public string SessionToken { get; set; }
        public int TaskId { get; set; }
    }
}