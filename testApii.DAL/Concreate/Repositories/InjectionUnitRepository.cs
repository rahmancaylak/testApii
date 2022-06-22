using System;
using System.Threading.Tasks;
using testApii.DAL.Interfaces;
using testApii.Entity;
using testApii.Entity.API;

namespace testApii.DAL.Repositories
{
    public class InjectionUnitRepository : GenericRepository<InjectionUnit>, IInjectionUnitRepository
    {
        private readonly IHelpers _helpers;
        public InjectionUnitRepository(TestDbContext context, IHelpers helpers) : base(context)
        {
            _helpers = helpers;
        }
        public async Task AddInjectionUnits()
        {
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 4
            };
            var newInjectionUnitList = new List<InjectionUnit>();
            var dbInjectionUnits = _context.InjectionUnits.ToList();
            var organizations = _context.Organizations.ToList();
            await Parallel.ForEachAsync(organizations, options, async (organization, _) =>
            {
                string injectionUnitJSONString = _helpers.CallAPI("InjectionUnitNames", organization.OrganizationETSOCode);
                var injectionUnitResponse = _helpers.Deserialize<Response<InjectionUnit>>(injectionUnitJSONString);
                var injectionUnits = dbInjectionUnits.Where(a => a.OrganizationETSOCode == organization.OrganizationETSOCode).ToList();
                await Parallel.ForEachAsync(injectionUnitResponse.Body.injectionUnits, options, async (APIInjectionUnit, _) =>
                {
                    var injectionUnit = injectionUnits.Where(eic => eic.EIC == APIInjectionUnit.EIC).FirstOrDefault();
                    if (injectionUnit == null)
                    {
                        newInjectionUnitList.Add(
                           new InjectionUnit()
                           {
                               UnitId = APIInjectionUnit.UnitId,
                               Name = APIInjectionUnit.Name,
                               EIC = APIInjectionUnit.EIC,
                               OrganizationETSOCode = organization.OrganizationETSOCode,
                           });
                    }
                    else if (injectionUnit.Name != APIInjectionUnit.Name)
                    {
                        injectionUnit.Name = APIInjectionUnit.Name;
                    }
                });
            });
            await AddRangeAsync(newInjectionUnitList);
            await _context.SaveChangesAsync();
        }
    }
}
