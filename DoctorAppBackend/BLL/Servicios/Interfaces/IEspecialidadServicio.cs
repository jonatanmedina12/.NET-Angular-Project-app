using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios.Interfaces
{
    public interface IEspecialidadServicio
    {
        Task<IEnumerable<EspecialidadDTo>> ObtenerTodos();
        Task<EspecialidadDTo> Agregar(EspecialidadDTo modeloDto);
        Task Actualizar (EspecialidadDTo modeloDto);

        Task Remover(int id);
    }
}
