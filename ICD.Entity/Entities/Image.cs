using ICD.Entity.Base;

namespace ICD.Entity.Entities;

public class Image : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? SliderId { get; set; }
    public Slider? Slider { get; set; }

}
