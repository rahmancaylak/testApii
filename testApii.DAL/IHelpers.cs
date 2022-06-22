using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApii.Entity;

namespace testApii.DAL
{
    public interface IHelpers
    {
        public string CallAPI(string _path, string parameter = "");
        public T Deserialize<T>(string jsonText);
        public string FindPath(string key);
        public (List<SantralValue> santralValue, string santralTipi) GetSantralValues(string parameters = "", string calculateType = "");
    }
}
