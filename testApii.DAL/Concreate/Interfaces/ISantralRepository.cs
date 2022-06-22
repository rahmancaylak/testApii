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
        public async Task AddSantral(InjectionUnit injectionUnit, string parameters) { }
    }
}
