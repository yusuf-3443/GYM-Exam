using System.Net;
using Domain.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.FileServices;

public class FileService:IFileService
{
    private readonly IWebHostEnvironment _hostEnvironment;

    public FileService(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }
    
    public async Task<string> CreateFile(IFormFile file)
    {
        try
        {
            var fileName = string.Format($"{Guid.NewGuid() + Path.GetExtension(file.Name)}");

            var fullPath = Path.Combine(_hostEnvironment.WebRootPath, "IMG", fileName);
            await using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new string(fileName);
        }   
        catch (Exception e)
        {
            return null;
        }
    }

    public bool DeleteFile(string file)
    {
        try
        {
            
            var fullPath = Path.Combine(_hostEnvironment.WebRootPath, "IMG", file);
            if (!File.Exists(fullPath))
                 return false;
            
            File.Delete(fullPath);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}