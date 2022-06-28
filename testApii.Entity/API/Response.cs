using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace testApii.Entity.API
{
    public class Response<T>
    {
        public string ResultCode { get; set; }
        public string ResultDescription { get; set; }
        [JsonIgnore]
        public Body<T> Body { get; set; }
        public List<T> Values { get; set; }
    }
}
