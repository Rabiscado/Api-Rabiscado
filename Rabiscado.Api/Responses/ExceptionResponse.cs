using System.Net;

namespace Rabiscado.Api.Responses;

public class ExceptionResponse : Response
{
    protected ExceptionResponse()
    {
        Title = "Ops, a server error occurred";
        Status = (int)HttpStatusCode.InternalServerError;
    }

    protected ExceptionResponse(string title) : this()
    {
        Title = title;
    }
}