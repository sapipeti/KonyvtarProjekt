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
    public static class KonyvDataProvider
    {
        private static string _url = "http://localhost:5000/api/konyvtar";

        public static IList<Konyv> GetKonyvek()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var konyvek = JsonConvert.DeserializeObject<IList<Konyv>>(rawData);
                    return konyvek;
                }
                else
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }

        }

        public static void CreateKonyv(Konyv konyv)
        {
            using(var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(konyv);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PostAsync(_url, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        public static void UpdateKonyv(Konyv konyv)
        {
            using (var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(konyv);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PutAsync(_url, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        public static void DeleteKonyv(int id)
        {
            using (var client = new HttpClient())
            {
                var response = client.DeleteAsync(_url + "/" + id).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }
    }
}
