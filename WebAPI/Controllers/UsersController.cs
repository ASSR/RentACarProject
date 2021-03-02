using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /*[HttpGet("getall")]
        public IActionResult Get()
        {
            var result = _userService.GetAll();

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);

            return result.Success ? Ok(result) : BadRequest(result);
        }*/

        /*[HttpPost("add")]
        public IActionResult Post(User user)
        {
            var result = _userService.Add(user);

            return result.Success ? Ok(result) : BadRequest(result);
        }*/
    }
}