using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
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

        [HttpGet("getallwithdetail")]
        public IActionResult GetAllWithDetail()
        {
            var result = _carService.GetAllWithDetail();

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}