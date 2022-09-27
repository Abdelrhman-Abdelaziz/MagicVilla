using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.DTOs;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Net;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        private APIResponse _response;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            this._response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _userRepo.Login(loginRequestDTO);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new() { "Username or Passwoard is incorrect" };
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        public IActionResult Login([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            bool uniqueUser = _userRepo.IsUniqueUser(registrationRequestDTO.UserName);
            if (!uniqueUser)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new() { "User Already Exist" };
                return BadRequest(_response);
            }

            var user = _userRepo.Register(registrationRequestDTO);
            if(user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new() { "Error While registering" };
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);

        }
    }
}
