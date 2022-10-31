namespace ICD.Business.DTO_s.Slider;

public class SliderGetDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public List<string?>? ImageName { get; set; }
    public DateTime CreatedDate { get; set; }
}
