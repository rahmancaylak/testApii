using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace testApii.Entity
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty("organizationId")]
        public int OrganizationId { get; set; }
        [JsonProperty("organizationName")]
        public string OrganizationName { get; set; }
        [JsonProperty("organizationStatus")]
        public string OrganizationStatus { get; set; }
        [JsonProperty("organizationETSOCode")]
        public string OrganizationETSOCode { get; set; }
        [JsonProperty("organizationShortName")]
        public string OrganizationShortName { get; set; }
    }
}