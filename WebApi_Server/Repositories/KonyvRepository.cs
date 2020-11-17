using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi_Common.Models;

namespace WebApi_Server.Repositories
{
    public class KonyvRepository
    {
        private const string filename = "Konyvek.json";

        public static IEnumerable<Konyv> GetPeople()
        {
            if (File.Exists(filename))
            {
                var rawData = File.ReadAllText(filename);
                var people = JsonSerializer.Deserialize<IEnumerable<Konyv>>(rawData);
                return people;
            }

            return new List<Konyv>();
        }

        public static void StorePeople(IEnumerable<Konyv> konyv)
        {
            var rawData = JsonSerializer.Serialize(konyv);
            File.WriteAllText(filename, rawData);
        }
    }
}
