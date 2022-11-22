using Microsoft.AspNetCore.Mvc.Rendering;

namespace Peluqueria.Models
{
    public class CitaCreacionViewModel : CitaViewModel
    {
        public IEnumerable<SelectListItem> TipoDeServicioViewModel { get; set; }

        public IEnumerable<SelectListItem> EstilistaViewModel { get; set; }
    }
}
