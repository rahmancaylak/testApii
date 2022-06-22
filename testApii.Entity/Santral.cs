using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace testApii.Entity
{
    public class Santral
    {
        [Key]
        public int Id { get; set; }
        public string UnitId { get; set; }
        public string Eic { get; set; }
        public string UnitName { get; set; }
        public string OrganizationETSOCode { get; set; }
        public SantralTipi SantralTipi { get; set; }
        [NotMapped]
        public Dictionary<string, List<SantralValuesResponse>> ValueList { get; set; }
    }

    public enum SantralTipi
    {
        Dogalgaz,
        Ruzgar,
        Linyit,
        Komur,
        FuelOil,
        Jeotermal,
        Barajli,
        Nafta,
        Biokutle,
        Akarsu,
        Unknown
    }
}
