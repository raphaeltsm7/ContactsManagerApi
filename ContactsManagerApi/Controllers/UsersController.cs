using ContactsManagerApi.Models;
using ContactsManagerApi.Models.DTO;
using ContactsManagerApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContactsManagerApi.Controllers
{
    [Route("/UsersAuth")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepo;
        protected APIResponse _response;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            this._response = new();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginResquestDTO model)
        {
            var loginResponse = await _userRepo.Login(model);
            if(loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token)) {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect." );
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationResquestDTO model)
        {
            return View();
        }
    }
}
