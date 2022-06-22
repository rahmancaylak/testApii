using System;
using System.Text;
using System.Threading.Tasks;
using testApii.DAL.Interfaces;
using testApii.Entity;
using testApii.Entity.API;

namespace testApii.DAL.Repositories
{
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        private readonly IHelpers _helpers;
        public OrganizationRepository(TestDbContext context, IHelpers helpers) : base(context)
        {
            _helpers = helpers;
        }

        public async Task AddOrganizations()
        {
            try
            {
                string organizationsJSONString = _helpers.CallAPI("Organizations");
                var organizationsResponse = _helpers.Deserialize<Response<Organization>>(organizationsJSONString);
                var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 4
                };
                var organizations = _context.Organizations.ToList();
                var newOrganizationList = new List<Organization>();
                await Parallel.ForEachAsync(organizationsResponse.Body.organizations, options, async (APIOrganization, _) =>
                {
                    var findOrganization = organizations.Where(a => a.OrganizationETSOCode == APIOrganization.OrganizationETSOCode).Any();
                    if (!findOrganization)
                    {
                        newOrganizationList.Add(
                            new Organization()
                            {
                                OrganizationId = APIOrganization.OrganizationId,
                                OrganizationName = APIOrganization.OrganizationName,
                                OrganizationShortName = APIOrganization.OrganizationShortName,
                                OrganizationETSOCode = APIOrganization.OrganizationETSOCode,
                                OrganizationStatus = APIOrganization.OrganizationStatus,
                            });
                    }
                });
                await AddRangeAsync(newOrganizationList);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                throw;
            }
        }
    }
}