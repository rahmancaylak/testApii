using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApii.DAL;
using testApii.Entity;
using testApii.Auth.Authorization;
using testApii.Entity.API;

namespace testApii.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly TestDbContext _context;

        public OrganizationsController(TestDbContext context)
        {
            _context = context;
        }

        // GET: api/Organizations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
        {
            var organizations = await _context.Organizations.ToListAsync();

            if (organizations == null)
            {
                return NotFound(new Response<Organization>()
                {
                    ResultCode = "404",
                    ResultDescription = "Organizations didn't find!",
                });
            }

            return Ok(new Response<Organization>()
            {
                ResultCode = "200",
                ResultDescription = "Success",
                Values = organizations,
            });
        }
    }
}
