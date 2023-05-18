using WebApi.Domain.Commands.Requests;
using WebApi.Domain.Commands.Responses;

namespace WebApi.Domain.Commands.Handlers
{
    public interface IHandler<Request, Response> 
        where Request : RequestBase 
        where Response : ResponseBase
    {
        Response Handle(Request request);
    }
}
