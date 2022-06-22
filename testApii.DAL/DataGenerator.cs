using testApii.DAL.Interfaces;

namespace testApii.DAL
{
    public class DataGenerator
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IInjectionUnitRepository _injectionUnitRepository;
        private readonly TestDbContext _context;
        public DataGenerator(TestDbContext context, IOrganizationRepository organizationRepository, IInjectionUnitRepository injectionUnitRepository)
        {
            _context = context;
            _organizationRepository = organizationRepository;
            _injectionUnitRepository = injectionUnitRepository;
        }
        public async Task Initialize()
        {
            if (_context.Organizations.Any() && _context.InjectionUnits.Any())
            {
                return;
            }
            await _organizationRepository.AddOrganizations();
            await _injectionUnitRepository.AddInjectionUnits();
        }
    }
}
