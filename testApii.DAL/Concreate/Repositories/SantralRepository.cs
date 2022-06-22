using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApii.DAL.Concreate.Interfaces;
using testApii.DAL.Repositories;
using testApii.Entity;

namespace testApii.DAL.Concreate.Repositories
{
    public class SantralRepository : GenericRepository<Santral>, ISantralRepository
    {
        private readonly IHelpers _helpers;
        public SantralRepository(TestDbContext context, IHelpers helpers) : base(context)
        {
            _helpers = helpers;
        }

        public async Task AddSantral(InjectionUnit injectionUnit, string parameters)
        {
            var eak = _helpers.GetSantralValues(parameters, "Eak");
            var kgup = _helpers.GetSantralValues(parameters, "Kgup");

            Dictionary<string, List<SantralValue>> ValueList = new Dictionary<string, List<SantralValue>>();
            ValueList.Add("Eak", eak.santralValue);
            ValueList.Add("Kgup", kgup.santralValue);

            var apiSantralTipi = eak.santralTipi;
            var santralTipi = Enum.TryParse(typeof(SantralTipi), apiSantralTipi, out var santralTipiValue);

            if (santralTipiValue == null)
            {
                santralTipiValue = SantralTipi.Unknown;
            }

            List<Santral> santralList = new List<Santral>();
            
            #region AddSantralList
            santralList.Add(new Santral()
            {
                UnitId = injectionUnit.UnitId,
                Eic = injectionUnit.EIC,
                UnitName = injectionUnit.Name,
                OrganizationETSOCode = injectionUnit.OrganizationETSOCode,
                SantralTipi = (SantralTipi)santralTipiValue,
                ValueList = ValueList
            });
            #endregion
            await AddRangeAsync(santralList);
            await _context.SaveChangesAsync();
        }
    }
}
