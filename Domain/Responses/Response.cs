using System.Net;

namespace Domain.Responses;

public class Response<T>
{
    public int StatusCode { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    public T? Data { get; set; }

    public Response(HttpStatusCode statusCode,List<string> errors,T data)
    {
        StatusCode = (int)statusCode;
        Errors = errors;
        Data = data;
    }
    public Response(HttpStatusCode statusCode,string errors,T data)
    {
        StatusCode = (int)statusCode;
        Errors.Add(errors);
        Data = data;
    }
    
    public Response(HttpStatusCode statusCode,List<string> errors)
    {
        StatusCode = (int)statusCode;
        Errors = errors;
    }
    
    public Response(HttpStatusCode statusCode,string errors)
    {
        StatusCode = (int)statusCode;
        Errors.Add(errors);
    }
    public Response(HttpStatusCode statusCode,T data)
    {
        StatusCode = (int)statusCode;
        Data = data;
    }

    public Response(T data)
    {
        StatusCode = 200;
        Data = data;
    }
}