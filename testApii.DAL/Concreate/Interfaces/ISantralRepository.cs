using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApii.DAL.Interfaces;
using testApii.Entity;

namespace testApii.DAL.Concreate.Interfaces
{
    public interface ISantralRepository : IGenericRepository<Santral>
    {
        public Task<List<Santral>> AddSantral(InjectionUnit injectionUnit, string parameters);
        public async Task UpdateSantral(Santral santral, InjectionUnit injectionUnit, string parameters) { }
    }
}
