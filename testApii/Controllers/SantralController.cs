using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApii.DAL;
using testApii.DAL.Concreate.Interfaces;
using testApii.Entity;

namespace testApii.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SantralController : ControllerBase
    {
        private readonly TestDbContext _context;
        private readonly IHelpers _helpers;
        private readonly ISantralRepository _santralRepository;
        public SantralController(TestDbContext context, IHelpers helpers, ISantralRepository santralRepository)
        {
            _context = context;
            _helpers = helpers;
            _santralRepository = santralRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Santral>>> GetSantral(string uevcbEIC, DateTime startDate, DateTime endDate)
        {
            var injectionUnits = await _context.InjectionUnits.ToListAsync();
            var injectionUnit = injectionUnits.Where(e => e.EIC == uevcbEIC).FirstOrDefault();

            #region ParametersErrorControl
            if (injectionUnit == null)
            {
                return NotFound(new { message = "Injection Unit doesn't find!" });
            }

            if (startDate > endDate)
            {
                return NotFound(new { message = "Start date can't be bigger than end date!" });
            }

            if (endDate > DateTime.Now)
            {
                return NotFound(new { message = "End date can't be bigger than today!" });
            }
            #endregion

            string parameters = $"?endDate={endDate:yyyy-MM-dd}&organizationEIC={injectionUnit.OrganizationETSOCode}&startDate={startDate:yyyy-MM-dd}&uevcbEIC={injectionUnit.EIC}";
            await _santralRepository.AddSantral(injectionUnit, parameters);
            var santrals = _context.Santrals.ToList();
            return santrals;
        }
    }
}
