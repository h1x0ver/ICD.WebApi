using ICD.Core.EFRepository;
using ICD.Data.Abstracts;
using ICD.Data.Context;
using ICD.Entity.Entities;

namespace ICD.Data.Implementations;

public class SliderRepositoryDal : EFEntityRepositoryBase<Slider,AppDbContext>, ISliderDal
{
    public SliderRepositoryDal(AppDbContext context) : base(context) { }
}
