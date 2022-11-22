using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Peluqueria.Models;
using Peluqueria.Servicios;
using System.Runtime.InteropServices;

namespace Peluqueria.Controllers
{
    public class CitaController : Controller
    {
        private readonly IRepositorioEstilista repositorioEstilista;
        private readonly IRepositorioTipoDeServicio repositorioTipoDeServicio;
        private readonly IRepositorioCita repositorioCita;
        private readonly IMapper _mapper;
        public CitaController(IRepositorioEstilista estilista, IRepositorioTipoDeServicio servicio, IRepositorioCita cita, IMapper mapper)
        {
            repositorioEstilista = estilista;
            repositorioTipoDeServicio = servicio;
            repositorioCita = cita;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var Cita = await repositorioCita.Obtener();
            return View(Cita);
        }


        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            
            var modelo = new CitaCreacionViewModel();
            modelo.TipoDeServicioViewModel = await ObtenerServicios();
            modelo.EstilistaViewModel = await ObtenerEstilistas();
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CitaCreacionViewModel citaCreacion)
        {
            if (!ModelState.IsValid)
            {
                citaCreacion.TipoDeServicioViewModel = await ObtenerServicios();
                citaCreacion.EstilistaViewModel = await ObtenerEstilistas();
                return View(citaCreacion);
                
            }

            var Existe = await repositorioCita.Existe(citaCreacion.IdEstilista,citaCreacion.HoraCita);

            if (Existe)
            {
                ModelState.AddModelError(nameof(citaCreacion.HoraCita), $"La hora y el dia seleccionados {citaCreacion.HoraCita} ya se encuentra apartados en el sistema");
                citaCreacion.TipoDeServicioViewModel = await ObtenerServicios();
                citaCreacion.EstilistaViewModel = await ObtenerEstilistas();
                return View(citaCreacion);
            }

            await repositorioCita.Crear(citaCreacion);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var cita = await repositorioCita.ObtenerPorId(id);

            if (cita is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var modelo = _mapper.Map<CitaCreacionViewModel>(cita);

            modelo.TipoDeServicioViewModel = await ObtenerServicios();
            modelo.EstilistaViewModel = await ObtenerEstilistas();
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(CitaCreacionViewModel citaVM)
        {

            var cita = await repositorioCita.ObtenerPorId(citaVM.Id);

            if (cita is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var Existe = await repositorioCita.Existe(citaVM.IdEstilista, citaVM.HoraCita);

            if (Existe)
            {
                ModelState.AddModelError(nameof(citaVM.HoraCita), $"La hora y el dia seleccionados {citaVM.HoraCita} ya se encuentra apartados en el sistema");
                citaVM.TipoDeServicioViewModel = await ObtenerServicios();
                citaVM.EstilistaViewModel = await ObtenerEstilistas();
                return View(citaVM);
            }


            await repositorioCita.Actualizar(citaVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Borrar(int id)
        {
            var cita = await repositorioCita.ObtenerPorId(id);

            if (cita is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(cita);

        }

        [HttpPost]
        public async Task<IActionResult> BorrarCita(int id)
        {
            var cita = await repositorioCita.ObtenerPorId(id);

            if (cita is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioCita.Borrar(id);
            return RedirectToAction("Index");
        }


        private async Task<IEnumerable<SelectListItem>> ObtenerServicios()
        {
            var tiposdeservicio = await repositorioTipoDeServicio.Obtener();
            return tiposdeservicio.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerEstilistas()
        {
            var estilistas = await repositorioEstilista.Obtener();
            return estilistas.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }
    }
}
