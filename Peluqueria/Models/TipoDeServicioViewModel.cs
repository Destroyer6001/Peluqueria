﻿using System.ComponentModel.DataAnnotations;

namespace Peluqueria.Models
{
    public class TipoDeServicioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "No puede ser mayor a {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
    }
}
