using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApi_Common.Models;

namespace WebApi_Common.DataProviders
{
    public static class FelhasznaloAdatDataProvider
    {
        private static string _url = "http://localhost:5000/api/konyvtar/adatok";

        public static FelhasznaloAdatok GetData(string neptunKod)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_url+"/"+ neptunKod).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var fAdat = JsonConvert.DeserializeObject<FelhasznaloAdatok>(rawData);
                    return fAdat;
                }
                else
                {
                    return new FelhasznaloAdatok(null,null);
                }
            }

        }

        public static IList<FelhasznaloAdatok> GetData()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var fAdat = JsonConvert.DeserializeObject<IList<FelhasznaloAdatok>>(rawData);
                    return fAdat;
                }
                else
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }

        }

        public static void CreateFelhasznalo(FelhasznaloAdatok fAdat)
        {
            using(var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(fAdat);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PostAsync(_url, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }
    }
}
