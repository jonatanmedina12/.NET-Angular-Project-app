using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class MedicoDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Apellidos es requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Apellidos debe ser mínimo 1 y máximo de 60 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Nombres es requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nombres debe ser mínimo 1 y máximo de 60 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Dirección es requerida")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Dirección debe ser mínimo 1 y máximo de 100 caracteres")]
        public string Direccion { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "Teléfono debe ser mínimo 1 y máximo de 40 caracteres")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Género es requerido")]
        [StringLength(1, ErrorMessage = "Género debe ser 1 caracter")]
        public string Genero { get; set; }

        public int Estado { get; set; }

        [Required(ErrorMessage = "Especialidad es requerida")]
        public int EspecialidadId { get; set; }

        public string NombreEspecialidad { get; set; }

    }
}
