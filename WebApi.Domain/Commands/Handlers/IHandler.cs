namespace WebApi.Domain.Commands.Handlers
{
    public interface IHandler<Request, Response> 
        where Request : class 
        where Response : class
    {
        Response Handle(Request request);
    }
}
