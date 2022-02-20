using Microsoft.AspNetCore.Mvc;
using TestDev.Model;
using TestDev.Services;

namespace TestDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        
        /// <summary>
        /// Get all users in the database.
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());
    }
}
