using ICD.Core.EFRepository;
using ICD.Data.Abstracts;
using ICD.Data.Context;
using ICD.Entity.Entities;

namespace ICD.Data.Implementations;

public class ImageRepositoryDal : EFEntityRepositoryBase<Image, AppDbContext> , IImageDal
{
    public ImageRepositoryDal(AppDbContext context) : base(context) { }
}
