using Microsoft.AspNetCore.Mvc;
using Peluqueria.Models;
using Peluqueria.Servicios;

namespace Peluqueria.Controllers
{
    public class EstilistaController : Controller
    {
        private readonly IRepositorioEstilista repositorioEstilista;
        public EstilistaController(IRepositorioEstilista estilista)
        {
            repositorioEstilista = estilista;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Estilistas = await repositorioEstilista.Obtener();
            return View(Estilistas);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(EstilistaViewModel estilistaView)
        {
            if(!ModelState.IsValid)
            {
                return View(estilistaView);
            }

            var Existe = await repositorioEstilista.Existe(estilistaView.Nombre);

            if(Existe)
            {
                ModelState.AddModelError(nameof(estilistaView.Nombre), $"El estilista {estilistaView.Nombre} ya se encuentra registrado");
                return View(estilistaView);
            }

            await repositorioEstilista.Crear(estilistaView);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Editar(int id)
        {
            
            var estilista = await repositorioEstilista.ObtenerId(id);

            if (estilista is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(estilista);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(EstilistaViewModel Estilista)
        {
            var tipocuentaexiste = await repositorioEstilista.ObtenerId(Estilista.Id);

            if (tipocuentaexiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioEstilista.Actualizar(Estilista);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Borrar(int id)
        {
            var Estilista = await repositorioEstilista.ObtenerId(id);

            if (Estilista is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(Estilista);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarEstilista(int id)
        {
            var Estilista = await repositorioEstilista.ObtenerId(id);

            if (Estilista is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioEstilista.Borrar(id);
            return RedirectToAction("Index");
        }
    }
}
