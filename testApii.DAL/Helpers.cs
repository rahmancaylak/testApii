using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using testApii.Entity;
using testApii.Entity.API;

namespace testApii.DAL
{
    public class Helpers : IHelpers
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Helpers(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public string CallAPI(string _path, string parameter = "")
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            string baseURL = FindPath("BaseURL");
            string path = FindPath(_path);
            HttpResponseMessage response = httpClient.GetAsync(baseURL + path + parameter).Result;
            if (response != null)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            return "Empty";
        }
        public T Deserialize<T>(string jsonText)
        {
            return JsonConvert.DeserializeObject<T>(jsonText);
        }
        public string FindPath(string key)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("BaseURL", "https://api.epias.com.tr/epias/exchange/transparency/production");
            dict.Add("Organizations", "/dpp-organization");
            dict.Add("InjectionUnitNames", "/dpp-injection-unit-name?organizationEIC=");
            dict.Add("Eak", "/aic");
            dict.Add("Kgup", "/dpp");
            return dict[key];
        }
        public (List<SantralValuesResponse> santralValue, string santralTipi) GetSantralValues(string parameters = "", string calculateType = "")
        {
            string jsonString = CallAPI(calculateType, parameters);
            var response = Deserialize<Response<SantralValue>>(jsonString);
            string apiSantralTipi = "";
            List<SantralValuesResponse> list = new List<SantralValuesResponse>();
            if (calculateType.ToLower() == "eak")
            {
                foreach (var aic in response.Body.aicList)
                {
                    var props = aic.GetType().GetProperties().ToList().Where(prop => !"Toplam".Contains(prop.Name)).ToList();
                    foreach (var prop in props)
                    {
                        var value = prop.GetValue(aic);
                        if (Double.TryParse(value.ToString(), out var parsed))
                        {
                            if (parsed > 0)
                            {
                                apiSantralTipi = prop.Name.ToString();
                            }
                        }
                    }
                    list.Add(new SantralValuesResponse()
                    {
                        tarih = aic.tarih,
                        Toplam = aic.Toplam
                    });
                }
                return (list, apiSantralTipi);
            }

            if (calculateType.ToLower() == "kgup")
            {
                foreach (var dpp in response.Body.dppList)
                {
                    var props = dpp.GetType().GetProperties().ToList().Where(prop => !"Toplam".Contains(prop.Name));
                    foreach (var prop in props)
                    {
                        var value = prop.GetValue(dpp);

                        if (Double.TryParse(value.ToString(), out var parsed))
                        {
                            if (parsed > 0)
                            {
                                apiSantralTipi = prop.Name.ToString();
                            }
                        }
                    }
                    list.Add(new SantralValuesResponse()
                    {
                        tarih = dpp.tarih,
                        Toplam = dpp.Toplam,
                    });
                }
                return (list, null);
            }
            return (null, null);
        }
    }
}