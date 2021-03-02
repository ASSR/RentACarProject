using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarService _carService;
        ICarImageService _carImageService;

        public CarsController(ICarService carService, ICarImageService carImageService)
        {
            _carService = carService;
            _carImageService = carImageService;
        }

        [HttpGet("getall")]
        public IActionResult Get()
        {
            var result = _carService.GetAll();

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetById(id);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Post(Car car)
        {
            var result = _carService.Add(car);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getcarimages")]
        public IActionResult GetByCarImages(int carId)
        {
            var result = _carImageService.GetByCarId(carId);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("Addcarimage")]
        public async Task<IActionResult> AddCarImage(CarImageDTO carImageDTO)
        {
            var result = await _carImageService.Add(carImageDTO);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("updatecarimage")]
        public async Task<IActionResult> UpdateCarImage(CarImageDTO carImageDTO)
        {
            var result = await _carImageService.Add(carImageDTO);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        [Route("DeleteCarImage")]
        public IActionResult DeleteCarImage(int carImageId)
        {
            var result = _carImageService.Delete(carImageId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}