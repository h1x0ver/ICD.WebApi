using AutoMapper;
using ICD.Business.DTO_s.Slider;
using ICD.Entity.Entities;

namespace ICD.Business.Mapping;

public class Map : Profile
{
    public Map()
    {
        CreateMap<Slider, SliderGetDto>()
          .ForMember(c => c.ImageName, c => c.Ignore())
          .ForMember(c => c.ImageName, c => c.MapFrom(c => c.Images!.Select(c => c.Name)));

        CreateMap<SliderCreateDto, Slider>();
    }
}
