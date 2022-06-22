using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApii.Entity
{
    public class AicList
    {
        public DateTime tarih { get; set; }
        [JsonProperty("toplam")]
        public string? Toplam { get; set; }
        [JsonProperty("dogalgaz")]
        public string? Dogalgaz { get; set; }
        [JsonProperty("ruzgar")]
        public string? Ruzgar { get; set; }
        [JsonProperty("linyit")]
        public string? Linyit { get; set; }
        [JsonProperty("tasKomur")]
        public string? TasKomur { get; set; }
        [JsonProperty("ithalKomur")]
        public string? IthalKomur { get; set; }
        [JsonProperty("fuelOil")]
        public string? FuelOil { get; set; }
        [JsonProperty("jeotermal")]
        public string? Jeotermal { get; set; }
        [JsonProperty("barajli")]
        public string? Barajli { get; set; }
        [JsonProperty("nafta")]
        public string? Nafta { get; set; }
        [JsonProperty("biokutle")]
        public string? Biokutle { get; set; }
        [JsonProperty("akarsu")]
        public string? Akarsu { get; set; }
        [JsonProperty("diger")]
        public string? Diger { get; set; }
    }

    public class Statistic
    {
        public DateTime tarih { get; set; }
        public double toplamAvg { get; set; }
        public double toplamMin { get; set; }
        public double toplamMax { get; set; }
        public double toplamSum { get; set; }
        public int dogalgazAvg { get; set; }
        public int dogalgazMin { get; set; }
        public int dogalgazMax { get; set; }
        public int dogalgazSum { get; set; }
        public int ruzgarAvg { get; set; }
        public int ruzgarMin { get; set; }
        public int ruzgarMax { get; set; }
        public int ruzgarSum { get; set; }
        public int linyitAvg { get; set; }
        public int linyitMin { get; set; }
        public int linyitMax { get; set; }
        public int linyitSum { get; set; }
        public int tasKomurAvg { get; set; }
        public int tasKomurMin { get; set; }
        public int tasKomurMax { get; set; }
        public int tasKomurSum { get; set; }
        public int ithalKomurAvg { get; set; }
        public int ithalKomurMin { get; set; }
        public int ithalKomurMax { get; set; }
        public int ithalKomurSum { get; set; }
        public int fuelOilAvg { get; set; }
        public int fuelOilMin { get; set; }
        public int fuelOilMax { get; set; }
        public int fuelOilSum { get; set; }
        public int jeotermalAvg { get; set; }
        public int jeotermalMin { get; set; }
        public int jeotermalMax { get; set; }
        public int jeotermalSum { get; set; }
        public int barajliAvg { get; set; }
        public int barajliMin { get; set; }
        public int barajliMax { get; set; }
        public int barajliSum { get; set; }
        public int naftaAvg { get; set; }
        public int naftaMin { get; set; }
        public int naftaMax { get; set; }
        public int naftaSum { get; set; }
        public int biokutleAvg { get; set; }
        public int biokutleMin { get; set; }
        public int biokutleMax { get; set; }
        public int biokutleSum { get; set; }
        public double akarsuAvg { get; set; }
        public double akarsuMin { get; set; }
        public double akarsuMax { get; set; }
        public double akarsuSum { get; set; }
        public int digerAvg { get; set; }
        public int digerMin { get; set; }
        public int digerMax { get; set; }
        public int digerSum { get; set; }
    }


}
