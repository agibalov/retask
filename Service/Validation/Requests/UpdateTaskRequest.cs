namespace Service.Validation.Requests
{
    public class UpdateTaskRequest
    {
        public string SessionToken { get; set; }
        public int TaskId { get; set; }
        public string TaskDescription { get; set; }
    }
}