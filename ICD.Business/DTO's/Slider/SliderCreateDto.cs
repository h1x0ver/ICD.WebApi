using Microsoft.AspNetCore.Http;
namespace ICD.Business.DTO_s.Slider;
public class SliderCreateDto
{
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public List<IFormFile>? ImageFiles { get; set; }
}
