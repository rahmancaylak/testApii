using Microsoft.AspNetCore.Mvc;
using testApii.DAL.Interfaces;
using testApii.Auth.Authorization;
using testApii.Entity.Users;
using testApii.DAL;
using testApii.Entity.API;
using testApii.Entity;

namespace testApii.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly TestDbContext _context;
        private readonly IJwtUtils _jwtUtils;
        public AuthController(IUserRepository userRepository, IJwtUtils jwtUtils, TestDbContext context)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            if (_context.Users.Any(x => x.Username == model.Username))
            {
                return Conflict(new Response<RegisterRequest>()
                {
                    ResultCode = "409",
                    ResultDescription = $"Username {model.Username} is already taken"
                });
            }
            _userRepository.Register(model);
            return Ok(new Response<RegisterRequest>()
            {
                ResultCode = "200",
                ResultDescription = "Registration successful"
            });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return Unauthorized(new Response<LoginRequest>()
                {
                    ResultCode = "401",
                    ResultDescription = "Username or password incorrect"
                });
            }

            var response = _userRepository.Login(model);
            response.Token = _jwtUtils.GenerateToken(user);
            Response.Cookies.Append("jwt", response.Token, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new Response<LoginResponse>()
            {
                ResultCode = "200",
                ResultDescription = "Success",
                Values = new List<LoginResponse>() { response }
            });
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            var jwt = Request.Cookies["jwt"];
            var userId = _jwtUtils.ValidateToken(jwt);
            if (userId == null)
            {
                return NotFound(new Response<User>()
                {
                    ResultCode = "404",
                    ResultDescription = "User didn't find",
                });
            }
            var user = _userRepository.GetById(userId.Value);
            return Ok(new Response<User>()
            {
                ResultCode = "200",
                ResultDescription = "Success",
                Values = new List<User>() { user }
            });
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new Response<User>()
            {
                ResultCode = "200",
                ResultDescription = "Logout successful"
            });
        }
    }

}
