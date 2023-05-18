namespace WebApi.Domain.Commands.Requests
{
    public abstract class RequestBase
    {
        public bool IsValid { get; set; }

        public List<string> Errors { get; set; } = new();

        public abstract void Validate();
    }
}
