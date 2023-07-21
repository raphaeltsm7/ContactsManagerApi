using ContactsManagerApi.Models.DTO;
using ContactsManagerApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManagerApi.Controllers
{
    [Route("/UsersAuth")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepo;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginResquestDTO model)
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationResquestDTO model)
        {
            return View();
        }
    }
}
