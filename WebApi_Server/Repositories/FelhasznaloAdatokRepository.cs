using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi_Common.Models;

namespace WebApi_Server.Repositories
{
    public static class FelhasznaloAdatokRepository
    {
        private const string filename = "Adatok.json";

        public static IEnumerable<FelhasznaloAdatok> GetData()
        {
            if (File.Exists(filename))
            {
                var rawData = File.ReadAllText(filename);
                var fAdatok = JsonSerializer.Deserialize<IEnumerable<FelhasznaloAdatok>>(rawData);
                return fAdatok;
            }

            return new List<FelhasznaloAdatok>();
        }

        public static void StoreData(IEnumerable<FelhasznaloAdatok> fAdatok)
        {
            var rawData = JsonSerializer.Serialize(fAdatok);
            File.WriteAllText(filename, rawData);
        }
    }
}
