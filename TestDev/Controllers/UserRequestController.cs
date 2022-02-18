using Microsoft.AspNetCore.Mvc;
using TestDev.Model;
using TestDev.Services;

namespace TestDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRequestController : ControllerBase
    {
        private readonly IUserRequestService _service;
        public UserRequestController(IUserRequestService service)
        {
            _service = service;
        }

        //Get the user in your service via HttpClient
        [HttpGet]
        public async Task<IActionResult> GetUserAsync(string id) => Ok(await _service.GetUserAsync(id));



        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] User user) =>
        //    Ok(await _service.Create(user));

        //[HttpGet]
        //public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update([FromBody] User userIn, int id)
        //{
        //    await _service.Update(userIn, id);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _service.Delete(id);
        //    return NoContent();
        //}

        //[HttpGet]
        //public async Task<IActionResult> Get() => Ok(await _service.Get());


    }
}







