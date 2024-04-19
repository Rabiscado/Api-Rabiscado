using Microsoft.AspNetCore.Http;

namespace Rabiscado.Application.Contracts;

public interface IFileService
{
    Task<List<string>> GetAllDocuments();
    Task<Stream?> GetDocument(string fileName);
    Task<string?> UploadFile(IFormFile file);
    Task<string?> UploadFile(MemoryStream file);
    Task<bool> DeleteDocument(string fileName);
}