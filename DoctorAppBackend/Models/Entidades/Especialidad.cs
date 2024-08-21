using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Especialidad
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(60,MinimumLength =1,ErrorMessage ="El nombre debe ser minimo 1 maximo 60")]
        public string NombreEspecialidad { get; set; }
        [Required]
        [StringLength(100,MinimumLength =1,ErrorMessage ="La descripcion debe ser minimo 1 maximo 100 ")]
        public string  Descripcion { get; set; }

        public bool Estado {  get; set; }

        public DateTime FechaCreacion { get; set; } 

        public DateTime FechaActualizacion { get; set; }

    }
}
