using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(string id) => Ok(await _service.GetUserAsync(id));

    }
}







