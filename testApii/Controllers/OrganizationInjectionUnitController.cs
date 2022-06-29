using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApii.Auth.Authorization;
using testApii.DAL;
using testApii.DAL.Interfaces;
using testApii.Entity;
using testApii.Entity.API;

namespace testApii.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationInjectionUnitController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IInjectionUnitRepository _injectionUnitRepository;

        public OrganizationInjectionUnitController(TestDbContext context,
            IOrganizationRepository organizationRepository, IInjectionUnitRepository injectionUnitRepository, IHelpers helpers)
        {
            _organizationRepository = organizationRepository;
            _injectionUnitRepository = injectionUnitRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUpdatedOrganizationInjectionUnit()
        {
            try
            {
                await _organizationRepository.AddOrganizations();
                await _injectionUnitRepository.AddInjectionUnits();
                return Ok(new Response<Organization>()
                {
                    ResultCode = "200",
                    ResultDescription = "Organizations and Injections data updated!"
                });
            }
            catch (Exception)
            {
                return NotFound(new Response<Organization>()
                {
                    ResultCode = "404",
                    ResultDescription = "Something went wrong... Please try again or contact with us!",
                });
            }

        }
    }
}
