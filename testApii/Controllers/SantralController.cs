using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApii.Auth.Authorization;
using testApii.DAL;
using testApii.DAL.Concreate.Interfaces;
using testApii.Entity;
using testApii.Entity.API;

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
            string message = "";

            #region ParametersErrorControl
            if (injectionUnit == null)
            {
                message = "Injection Unit doesn't find!";
            }

            if (startDate > endDate)
            {
                message = "Start date can't be bigger than end date!";
            }

            if (endDate > DateTime.Now)
            {
                message = "End date can't be bigger than today!";
            }
            #endregion

            if (message.Length > 0)
            {
                return NotFound(new Response<Santral>()
                {
                    ResultCode = "404",
                    ResultDescription = message
                });
            }

            string parameters = $"?endDate={endDate:yyyy-MM-dd}&organizationEIC={injectionUnit.OrganizationETSOCode}&startDate={startDate:yyyy-MM-dd}&uevcbEIC={injectionUnit.EIC}";
            var santral = _santralRepository.AddSantral(injectionUnit, parameters);

            if (santral.Result.Count <= 0)
            {
                return NotFound(new Response<Santral>()
                {
                    ResultCode = "404",
                    ResultDescription = "Santral didn't find!"
                });
            }

            return Ok(new Response<Santral>()
            {
                ResultCode = "200",
                ResultDescription = "Success",
                Values = santral.Result
            });
        }
    }
}
