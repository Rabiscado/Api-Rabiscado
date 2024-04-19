using Microsoft.AspNetCore.Http;

namespace Rabiscado.Application.Dtos.V1.Class;

public class CreateClassDto
{
    public string Name { get; set; } = null!;
    public string? Descripton { get; set; }
    public string? Video { get; set; }
    public string? Music { get; set; }
    public int ModuleId { get; set; }
    public IFormFile? TumbImage { get; set; }
    public IFormFile? GifClass { get; set; }
    public List<IFormFile> ImagesClass { get; set; } = new();
}