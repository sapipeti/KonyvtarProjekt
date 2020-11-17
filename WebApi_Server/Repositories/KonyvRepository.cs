using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi_Common.Models;

namespace WebApi_Server.Repositories
{
    public static class KonyvRepository
    {
        private const string filename = "Konyvek.json";

        public static IEnumerable<Konyv> GetBooks()
        {
            if (File.Exists(filename))
            {
                var rawData = File.ReadAllText(filename);
                var konyvek = JsonSerializer.Deserialize<IEnumerable<Konyv>>(rawData);
                return konyvek;
            }

            return new List<Konyv>();
        }

        public static void StoreBooks(IEnumerable<Konyv> konyvek)
        {
            var rawData = JsonSerializer.Serialize(konyvek);
            File.WriteAllText(filename, rawData);
        }
    }
}
