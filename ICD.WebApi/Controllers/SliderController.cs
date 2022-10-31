using ICD.Business.DTO_s.Slider;
using ICD.Business.Services;
using ICD.Exceptions.EntityExceptions;
using ICD.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _sliderService.GetAll();
                return Ok(data);
            }
            catch (EntityCouldNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response(4222, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response(4000, ex.Message));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var data = await _sliderService.Get(id);
                return Ok(data);
            }
            catch (EntityCouldNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response(4222, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response(4000, ex.Message));
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromForm] SliderCreateDto entity)
        {
            try
            {
                await _sliderService.Create(entity);
                return NoContent();
            }
            catch (EntityCouldNotFoundException ex)
            {

                return StatusCode(StatusCodes.Status404NotFound, new Response(4991, ex.Message));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response(4100, ex.Message));
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.Delete(id);
            return NoContent();
        }


    }

}
