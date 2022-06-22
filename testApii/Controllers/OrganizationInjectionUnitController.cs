using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApii.DAL;
using testApii.DAL.Interfaces;
using testApii.Entity;
using testApii.Entity.API;

namespace testApii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationInjectionUnitController : ControllerBase
    {
        private readonly TestDbContext _context;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IInjectionUnitRepository _injectionUnitRepository;
        private readonly IHelpers _helpers;

        public OrganizationInjectionUnitController(TestDbContext context,
            IOrganizationRepository organizationRepository, IInjectionUnitRepository injectionUnitRepository, IHelpers helpers)
        {
            _context = context;
            _organizationRepository = organizationRepository;
            _injectionUnitRepository = injectionUnitRepository;
            _helpers = helpers;
        }

        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            try
            {
                List<string> list = new List<string>();
                string organizationJSONString = _helpers.CallAPI("Organizations");
                var response = _helpers.Deserialize<Response<Organization>>(organizationJSONString);
                var organizations = await _context.Organizations.ToListAsync();
                var apiOrganizations = response.Body.organizations.ToList();
                var dbInjectionUnits = _context.InjectionUnits.ToList();

                var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 4
                };

                await Parallel.ForEachAsync(organizations, options, async (dbOrganization, _) =>
                {
                    //var removeOrganization = apiOrganizations.Where(e => e.OrganizationETSOCode == dbOrganization.OrganizationETSOCode).Any();
                    //if (!removeOrganization)
                    //{
                    //    _organizationRepository.Remove(dbOrganization);
                    //}

                    string injectionUnitJSONString = _helpers.CallAPI("InjectionUnitNames", dbOrganization.OrganizationETSOCode);
                    var injectionUnitResponse = _helpers.Deserialize<Response<InjectionUnit>>(injectionUnitJSONString);
                    var injectionUnits = dbInjectionUnits.Where(a => a.OrganizationETSOCode == dbOrganization.OrganizationETSOCode).ToList();

                    await Parallel.ForEachAsync(injectionUnitResponse.Body.injectionUnits, options, async (APIInjectionUnit, _) =>
                    {
                        var removeInjectionUnit = injectionUnits.Where(e => e.EIC == APIInjectionUnit.EIC).Any();
                        if (!removeInjectionUnit)
                        {
                            _injectionUnitRepository.RemoveRange(injectionUnits);
                        }
                    });

                });
                await _context.SaveChangesAsync();
                await _organizationRepository.AddOrganizations();
                await _injectionUnitRepository.AddInjectionUnits();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return NotFound();
                throw;
            }
        }
    }
}
