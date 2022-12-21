using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllPhiAPI.Controllers
{
    [Route("Login")]
    [ApiController]
    public class UserController : Controller
    {
        //webapi using iuserRepo
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // bool login

        [HttpGet("{username}/{password}", Name = "Login")]
        public IActionResult Login(string username, string password)
        {
            var user = _userRepository.Login(username, password);
            return Ok(user);
        }
        
        
    }
}
