using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebmotorsWebApp.Data;
using WebmotorsWebApp.Models;
using WebmotorsWebApp.Repository;

namespace WebmotorsWebApp.Controllers
{
    public class AnunciosController : Controller
    {
        private readonly WebmotorsContext _context;

        public AnunciosController(WebmotorsContext context)
        {
            _context = context;
        }

        // GET: Anuncios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Anuncios.ToListAsync());
        }

        // GET: Anuncios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncios.FirstOrDefaultAsync(m => m.Id == id);

            if (anuncio == null)
            {
                return NotFound();
            }

            return View(anuncio);
        }

        // GET: Anuncios/Create
        public async Task<IActionResult> Create()
        {
            var marcas = await GetMarcas();

            ViewBag.Marcas = marcas;

            return View();
        }

        // GET: Anuncios/GetMarcas
        public async Task<List<SelectListItem>> GetMarcas()
        {
            var apiRepository = new ApiRepository();

            var marcas = new List<SelectListItem> { new SelectListItem { Value = 0.ToString(), Text = "" } };

            foreach (var marca in await apiRepository.GetMarcasAsync())
            {
                var item = new SelectListItem
                {
                    Value = marca.Id.ToString(),
                    Text = marca.Name
                };

                marcas.Add(item);
            }

            return marcas;
        }

        // GET: Anuncios/GetModelos
        public async Task<List<SelectListItem>> GetModelos(string idMarca)
        {
            var apiRepository = new ApiRepository();

            var modelos = new List<SelectListItem> { new SelectListItem { Value = 0.ToString(), Text = "" } };

            foreach (var modelo in await apiRepository.GetModelosAsync(idMarca))
            {
                var item = new SelectListItem
                {
                    Value = modelo.Id.ToString(),
                    Text = modelo.Name
                };

                modelos.Add(item);
            }

            return modelos;
        }

        // GET: Anuncios/GetVersoes
        public async Task<List<SelectListItem>> GetVersoes(string idModelo)
        {
            var apiRepository = new ApiRepository();

            var versoes = new List<SelectListItem> { new SelectListItem { Value = 0.ToString(), Text = "" } };

            foreach (var versao in await apiRepository.GetVersoesAsync(idModelo))
            {
                var item = new SelectListItem
                {
                    Value = versao.Id.ToString(),
                    Text = versao.Name
                };

                versoes.Add(item);
            }

            return versoes;
        }

        // POST: Anuncios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Anuncio anuncio, string nomeMarca, string nomeModelo, string nomeVersao)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(nomeMarca))
                {
                    anuncio.Marca = nomeMarca;
                }

                if (!string.IsNullOrEmpty(nomeModelo))
                {
                    anuncio.Modelo = nomeModelo;
                }

                if (!string.IsNullOrEmpty(nomeVersao))
                {
                    anuncio.Versao = nomeVersao;
                }

                _context.Add(anuncio);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(anuncio);
        }

        // GET: Anuncios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncios.FindAsync(id);

            if (anuncio == null)
            {
                return NotFound();
            }

            var marcas = await GetMarcas();

            ViewBag.Marcas = marcas;

            return View(anuncio);
        }

        // POST: Anuncios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Anuncio anuncio, string nomeMarca, string nomeModelo, string nomeVersao)
        {
            if (id != anuncio.Id)
            {
                return NotFound();
            }

            if (nomeMarca == null)
            {
                ModelState.AddModelError("Marca", "The Marca field is required.");
            }

            if (nomeModelo == null)
            {
                ModelState.AddModelError("Modelo", "The Modelo field is required.");
            }

            if (nomeVersao == null)
            {
                ModelState.AddModelError("Versao", "The Versao field is required.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(nomeMarca))
                    {
                        anuncio.Marca = nomeMarca;
                    }

                    if (!string.IsNullOrEmpty(nomeModelo))
                    {
                        anuncio.Modelo = nomeModelo;
                    }

                    if (!string.IsNullOrEmpty(nomeVersao))
                    {
                        anuncio.Versao = nomeVersao;
                    }

                    _context.Update(anuncio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnuncioExists(anuncio.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            var marcas = await GetMarcas();

            ViewBag.Marcas = marcas;

            return View(anuncio);
        }

        // GET: Anuncios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncios.FirstOrDefaultAsync(m => m.Id == id);

            if (anuncio == null)
            {
                return NotFound();
            }

            return View(anuncio);
        }

        // POST: Anuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anuncio = await _context.Anuncios.FindAsync(id);
            _context.Anuncios.Remove(anuncio);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool AnuncioExists(int id)
        {
            return _context.Anuncios.Any(e => e.Id == id);
        }
    }
}