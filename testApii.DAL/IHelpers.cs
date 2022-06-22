using System;
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
        public (List<SantralValuesResponse> santralValue, string santralTipi) GetSantralValues(string parameters = "", string calculateType = "");
    }
}
