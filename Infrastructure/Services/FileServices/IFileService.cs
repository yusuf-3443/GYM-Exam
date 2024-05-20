using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.FileServices;

public interface IFileService
{
    Task<string> CreateFile(IFormFile file);
    bool DeleteFile(string file);
}