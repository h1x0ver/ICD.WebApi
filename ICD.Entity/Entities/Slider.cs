using ICD.Entity.Base;

namespace ICD.Entity.Entities;

public class Slider : BaseEntity, IEntity
{
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public ICollection<Image>? Images { get; set; }
}
