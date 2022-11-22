using Microsoft.AspNetCore.Mvc;
using Peluqueria.Models;
using Peluqueria.Servicios;

namespace Peluqueria.Controllers
{
    public class TipoDeServicioController:Controller
    {
        private readonly IRepositorioTipoDeServicio servicio;
        public TipoDeServicioController(IRepositorioTipoDeServicio tipoDeServicio)
        {
            servicio = tipoDeServicio;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var TipoDeServicio = await servicio.Obtener();
            return View(TipoDeServicio);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TipoDeServicioViewModel servicioVM)
        {
            if (!ModelState.IsValid)
            {
                return View(servicioVM);
            }

            var Existe = await servicio.Existe(servicioVM.Nombre);

            if (Existe)
            {
                ModelState.AddModelError(nameof(servicioVM.Nombre), $"El tipo de servicio {servicioVM.Nombre} ya se encuentra registrado");
                return View(servicioVM);
            }

            await servicio.Crear(servicioVM);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Editar(int id)
        {

            var TipoDeServicio = await servicio.ObtenerId(id);

            if (TipoDeServicio is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(TipoDeServicio);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(TipoDeServicioViewModel tipoDeServicio)
        {
            var TipoDeServicio = await servicio.ObtenerId(tipoDeServicio.Id);

            if (TipoDeServicio is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await servicio.Actualizar(tipoDeServicio);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Borrar(int id)
        {
            var TipoDeServicio = await servicio.ObtenerId(id);

            if (TipoDeServicio is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(TipoDeServicio);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarTipoDeServicio(int id)
        {
            var TipoDeServicio= await servicio.ObtenerId(id);

            if (TipoDeServicio is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await servicio.Borrar(id);
            return RedirectToAction("Index");
        }
    }
}
