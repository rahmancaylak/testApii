using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApii.DAL;
using testApii.Entity;
using testApii.Auth.Authorization;

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
            if (_context.Organizations == null)
            {
                return NotFound();
            }

            return await _context.Organizations.ToListAsync();
        }
    }
}
