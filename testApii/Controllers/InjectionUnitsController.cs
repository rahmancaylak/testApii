using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApii.Auth.Authorization;
using testApii.DAL;
using testApii.Entity;
using testApii.Entity.API;

namespace testApii.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InjectionUnitsController : ControllerBase
    {
        private readonly TestDbContext _context;

        public InjectionUnitsController(TestDbContext context)
        {
            _context = context;
        }

        // GET: api/InjectionUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InjectionUnit>>> GetInjectionUnitNames()
        {

            var injectionUnits = await _context.InjectionUnits.ToListAsync();

            if (injectionUnits == null)
            {
                return NotFound(new Response<InjectionUnit>()
                {
                    ResultCode = "404",
                    ResultDescription = "Injection Units didn't find!",
                });
            }

            return Ok(new Response<InjectionUnit>()
            {
                ResultCode = "200",
                ResultDescription = "Success",
                Values = injectionUnits,
            });
        }
    }
}
