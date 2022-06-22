using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace testApii.Entity
{
    public class InjectionUnit
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty("id")]
        public string UnitId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("eic")]
        public string EIC { get; set; }
        public string? OrganizationETSOCode { get; set; }
    }
}
