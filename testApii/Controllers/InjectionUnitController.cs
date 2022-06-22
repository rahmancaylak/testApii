using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApii.Auth.Authorization;
using testApii.DAL;
using testApii.Entity;

namespace testApii.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InjectionUnitController : ControllerBase
    {
        private readonly TestDbContext _context;

        public InjectionUnitController(TestDbContext context)
        {
            _context = context;
        }

        // GET: api/InjectionUnitNames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InjectionUnit>>> GetInjectionUnitNames()
        {
          if (_context.InjectionUnits == null)
          {
              return NotFound();
          }
            return await _context.InjectionUnits.ToListAsync();
        }
    }
}
