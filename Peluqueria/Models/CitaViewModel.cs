using System.ComponentModel.DataAnnotations;

namespace Peluqueria.Models
{
    public class CitaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo requerido")]
        public string Apellidos { get; set; }

        public string Telefono { get; set; }

        [Display(Name = "Hora y Dia")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "El campo {0} es un campo requerido")]
        public DateTime HoraCita { get; set; } = DateTime.Parse(DateTime.Now.ToString("G"));

        [Required(ErrorMessage = "El campo {0} es un campo requerido")]
        [Display(Name = "Tipo De Servicio")]
        public int IdTipoDeServicio { get; set; }

        public bool Estado { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo requerido")]
        [Display(Name = "Estilista")]
        public int IdEstilista { get; set; }

        public string NombreEstilista { get; set; }

        public string NombreTipoDECuenta { get; set; }
    }
}
