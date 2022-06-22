using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApii.Entity;

namespace testApii.DAL.Interfaces
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        public async Task AddOrganizations() { }
    }
}
