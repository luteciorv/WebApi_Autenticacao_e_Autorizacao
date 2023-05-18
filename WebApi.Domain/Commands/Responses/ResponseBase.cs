namespace WebApi.Domain.Commands.Responses
{
    public abstract class ResponseBase
    {
        public bool Success { get; set; }
        public object? Message { get; set; }
    }
}
