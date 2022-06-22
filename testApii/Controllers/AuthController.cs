using Microsoft.AspNetCore.Mvc;
using testApii.DAL.Interfaces;
using testApii.Auth.Authorization;
using testApii.Entity.Users;

namespace testApii.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;
        public AuthController(IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            _userRepository.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest model)
        {
            var response = _userRepository.Login(model);
            var user = _userRepository.GetByUsername(model.Username);
            response.Token = _jwtUtils.GenerateToken(user);

            Response.Cookies.Append("jwt", response.Token, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(response);
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new { message = "Logout successful" });
        }
    }

}
