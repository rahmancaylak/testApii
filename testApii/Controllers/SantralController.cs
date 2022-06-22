using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApii.Auth.Authorization;
using testApii.DAL;
using testApii.DAL.Concreate.Interfaces;
using testApii.Entity;

namespace testApii.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<Santral>> GetSantral(string uevcbEIC, DateTime startDate, DateTime endDate)
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

            var santral = _context.Santrals.Where(e => e.Eic == uevcbEIC).FirstOrDefault();

            if (santral == null)
            {
                await _santralRepository.AddSantral(injectionUnit, parameters);
                santral = _context.Santrals.Where(e => e.Eic == uevcbEIC).FirstOrDefault();
                return santral;
            }
            if (((int)santral.SantralTipi) == 0)
            {
                await _santralRepository.UpdateSantral(santral, injectionUnit, parameters);
            }

            var eak = _helpers.GetSantralValues(parameters, "Eak");
            var kgup = _helpers.GetSantralValues(parameters, "Kgup");

            Dictionary<string, List<SantralValuesResponse>> ValueList = new Dictionary<string, List<SantralValuesResponse>>();
            ValueList.Add("Eak", eak.santralValue);
            ValueList.Add("Kgup", kgup.santralValue);

            santral.ValueList = ValueList;
            return santral;
        }
    }
}
