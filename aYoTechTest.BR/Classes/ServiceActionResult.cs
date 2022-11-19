namespace aYoTechTest.BR.Classes
{
    public class ServiceActionResult<T>
    {
        public T Data { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public ServiceActionResult(T entity)
        {
            Data = entity;
        }

        public ServiceActionResult(T entity, string message, bool status)
        {
            Status = status;
            Message = message;
            Data = entity;
        }
    }
}
