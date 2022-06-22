using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApii.Entity
{
    public class SantralValue
    {
        public DateTime tarih { get; set; }
        [JsonProperty("toplam")]
        public decimal Toplam { get; set; }
        [JsonProperty("dogalgaz")]
        public decimal Dogalgaz { get; set; }
        [JsonProperty("ruzgar")]
        public decimal Ruzgar { get; set; }
        [JsonProperty("linyit")]
        public decimal Linyit { get; set; }
        [JsonProperty("tasKomur")]
        public decimal TasKomur { get; set; }
        [JsonProperty("ithalKomur")]
        public decimal IthalKomur { get; set; }
        [JsonProperty("fuelOil")]
        public decimal FuelOil { get; set; }
        [JsonProperty("jeotermal")]
        public decimal Jeotermal { get; set; }
        [JsonProperty("barajli")]
        public decimal Barajli { get; set; }
        [JsonProperty("nafta")]
        public decimal Nafta { get; set; }
        [JsonProperty("biokutle")]
        public decimal Biokutle { get; set; }
        [JsonProperty("akarsu")]
        public decimal Akarsu { get; set; }
        [JsonProperty("diger")]
        public decimal Diger { get; set; }
    }
}
