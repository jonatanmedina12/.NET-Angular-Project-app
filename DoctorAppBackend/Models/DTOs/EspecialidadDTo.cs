using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class EspecialidadDTo
    {

        public int Id { get; set; }
        [Required(ErrorMessage ="eL NOMBRE ES REQUERIDOo")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "El nombre debe ser minimo 1 maximo 60")]
        public string NombreEspecialidad { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "La descripcion debe ser minimo 1 maximo 100 ")]
        public string Descripcion { get; set; }

        public int Estado { get; set; }


    }
}
