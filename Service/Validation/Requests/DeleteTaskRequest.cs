namespace Service.Validation.Requests
{
    public class DeleteTaskRequest
    {
        public string SessionToken { get; set; }
        public int TaskId { get; set; }
    }
}