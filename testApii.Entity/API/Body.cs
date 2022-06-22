using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApii.Entity.API
{
    public class Body<T>
    {
        public List<T> organizations { get; set; }
        [JsonProperty("injectionUnitNames")]
        public List<T> injectionUnits { get; set; }
        public List<T> aicList { get; set; }
        public List<T> dppList { get; set; }
    }
}
