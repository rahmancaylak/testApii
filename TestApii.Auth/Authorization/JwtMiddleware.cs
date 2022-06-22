using Microsoft.AspNetCore.Http;
using testApii.DAL.Interfaces;

namespace testApii.Auth.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository, IJwtUtils jwtService)
        {
            var jwtCookie = context.Request.Cookies["jwt"];
            var userId = jwtService.ValidateToken(jwtCookie);

            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userRepository.GetById(userId.Value);
            }

            await _next(context);
        }
    }
}
