using Microsoft.AspNetCore.Http;
namespace ICD.Business.DTO_s.Slider;
public class SliderUpdateDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public IFormFile? ImageFile { get; set; }
}
