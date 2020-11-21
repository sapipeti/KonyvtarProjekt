using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Common.Models;
using WebApi_Server.Repositories;

namespace WebApi_Server.Controllers
{
    [Route("api/konyvtar/konyvek")]
    [ApiController]
    public class KonyvtarController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Konyv>> Get()
        {
            var konyvek = KonyvRepository.GetBooks();
            return Ok(konyvek);
        }

        [HttpGet("{id}")]
        public ActionResult<Konyv> Get(int id)
        {
            var konyvek = KonyvRepository.GetBooks();

            var konyv = konyvek.FirstOrDefault(x => x.Id == id);
            if (konyv != null)
            {
                return Ok(konyv);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult Post([FromBody] Konyv konyv)
        {
            var konyvek = KonyvRepository.GetBooks().ToList();

            konyv.Id = GetNewId(konyvek);
            konyvek.Add(konyv);

            KonyvRepository.StoreBooks(konyvek);
            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Konyv konyv)
        {
            var konyvek = KonyvRepository.GetBooks().ToList();

            var bookToUpdate = konyvek.FirstOrDefault(k => k.Id == konyv.Id);
            if (bookToUpdate != null)
            {
                bookToUpdate.Cím = konyv.Cím;
                bookToUpdate.Darabszám = konyv.Darabszám;
                bookToUpdate.ISBN = konyv.ISBN;
                bookToUpdate.Kiadás_Év = konyv.Kiadás_Év;
                bookToUpdate.Kiadó = konyv.Kiadó;
                bookToUpdate.Műfajok = konyv.Műfajok;
                bookToUpdate.Szerző = konyv.Szerző;
                bookToUpdate.VisszaHozas = konyv.VisszaHozas;
                bookToUpdate.NeptunKod = konyv.NeptunKod;
                bookToUpdate.KolcsonzottDB = konyv.KolcsonzottDB;

                KonyvRepository.StoreBooks(konyvek);
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var konyvek = KonyvRepository.GetBooks().ToList();

            var bookToDelete = konyvek.FirstOrDefault(k => k.Id == id);
            if (bookToDelete != null)
            {
                konyvek.Remove(bookToDelete);

                KonyvRepository.StoreBooks(konyvek);
                return Ok();
            }

            return NotFound();
        }

        private long GetNewId(IEnumerable<Konyv> konyvek)
        {
            long newId = 0;
            foreach (var konyv in konyvek)
            {
                if (newId < konyv.Id)
                {
                    newId = konyv.Id;
                }
            }
            return newId + 1;
        }
    }
}
