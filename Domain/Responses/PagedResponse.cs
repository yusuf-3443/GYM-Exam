using System.Net;

namespace Domain.Responses;

public class PagedResponse<T>:Response<T>
{
    public int PageNumber { get; set; } 
    public int PageSize { get; set; } 
    public int TotalPages { get; set; } 
    public int TotalRecords { get; set; } 

    public PagedResponse(T data, int totalRecords, int pageNumber, int pageSize) : base(data)
    {
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    
    public PagedResponse(HttpStatusCode statusCode, List<string> errors, T data) : base(statusCode, errors, data)
    {
    }

    public PagedResponse(HttpStatusCode statusCode, string errors, T data) : base(statusCode, errors, data)
    {
    }

    public PagedResponse(HttpStatusCode statusCode, List<string> errors) : base(statusCode, errors)
    {
    }

    public PagedResponse(HttpStatusCode statusCode, string errors) : base(statusCode, errors)
    {
    }

    public PagedResponse(HttpStatusCode statusCode, T data) : base(statusCode, data)
    {
    }

    public PagedResponse(T data) : base(data)
    {
    }
}