using AutoMapper;
using ICD.Business.DTO_s.Slider;
using ICD.Business.Helper;
using ICD.Business.Services;
using ICD.Data.Abstracts;
using ICD.Entity.Entities;
using ICD.Exceptions.EntityExceptions;
using Microsoft.Extensions.Hosting;

namespace ICD.Business.Repositories;

public class SliderRepository : ISliderService
{
    private readonly ISliderDal _sliderDal;
    private readonly IMapper _mapper;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IImageDal _imageDal;

    public SliderRepository(ISliderDal sliderDal, IMapper mapper, IHostEnvironment hostEnvironment, IImageDal imageDal)
    {
        _sliderDal = sliderDal;
        _mapper = mapper;
        _hostEnvironment = hostEnvironment;
        _imageDal = imageDal;
    }


    public async Task<SliderGetDto> Get(int id)
    {
        var data = await _sliderDal.GetAsync(n => n.Id == id && !n.isDeleted, 0,"Images");
        if (data is null) throw new EntityCouldNotFoundException();
        return _mapper.Map<SliderGetDto>(data);
    }
    public async Task<List<SliderGetDto>> GetAll()
    {
        var datas = await _sliderDal.GetAllAsync(orderBy: n => n.CreatedDate, n => !n.isDeleted, 0, int.MaxValue,  "Images");
        if (datas is null) throw new EntityCouldNotFoundException();
        return _mapper.Map<List<SliderGetDto>>(datas);
    }
    public async Task Create(SliderCreateDto entity)
    {
        Slider data = _mapper.Map<Slider>(entity);
        data.CreatedDate = DateTime.UtcNow.AddHours(4);
        if (entity.ImageFiles != null)
        {
            List<Image> images = new();
            foreach (var imageFile in entity.ImageFiles)
            {
                Image image = new()
                {
                    Name = await imageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images")
                };
                await _imageDal.CreateAsync(image);
                images.Add(image);
            }
            data.Images = images;
        }
        await _sliderDal.CreateAsync(data);
    }

    public async Task Update(int id, SliderUpdateDto entity)
    {
        var data = await _sliderDal.GetAsync(n => n.Id == entity.Id,0, "Images");
        if (data is null) throw new NullReferenceException();
        Slider datas = await _sliderDal.GetAsync(u => u.Id == entity.Id);
        if (datas is not null) throw new ArgumentException();

        if (entity.Title is not null && entity.Title?.Trim() != "")
        {
            data.Title = entity.Title?.Trim();
        }

        if (entity.SubTitle is not null && entity.SubTitle?.Trim() != "")
        {
            data.SubTitle = entity.SubTitle?.Trim();
        }
        var image = new Image
        {
            Name = await entity.ImageFile!.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images")
        };
        await _imageDal.CreateAsync(image);
        await _sliderDal.SaveAsync();
    }
    public async Task Delete(int id)
    {
        Slider slider = await _sliderDal.GetAsync(n => n.Id == id, includes: "Images");
        if (slider == null) throw new NullReferenceException();
        //await _postDal.DeleteAsync(post);
        slider.isDeleted = true;
        await _sliderDal.SaveAsync();
    }
}
