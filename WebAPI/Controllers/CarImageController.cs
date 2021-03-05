using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImageController : ControllerBase
    {
        ICarImageService _carImageService;

        public CarImageController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromForm(Name = ("ImageFile"))] IFormFile file, [FromForm] CarImageAddDto carImageAddDto)
        {
            var result = await _carImageService.Add(file, carImageAddDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getcarimages")]
        public IActionResult GetByCarImages(int carId)
        {
            var result = _carImageService.GetByCarId(carId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateCarImage([FromForm(Name = ("ImageFile"))] IFormFile file, [FromForm] CarImageUpdateDto carImageUpdateDto)
        {
            var result = await _carImageService.Update(file, carImageUpdateDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteCarImage(CarImageUpdateDto carImageUpdateDto)
        {
            var result = _carImageService.Delete(carImageUpdateDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}