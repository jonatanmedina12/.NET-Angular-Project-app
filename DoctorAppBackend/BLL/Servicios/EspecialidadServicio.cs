using AutoMapper;
using BLL.Servicios.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios
{
    public class EspecialidadServicio : IEspecialidadServicio
    {

        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;

        public EspecialidadServicio(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }


        public async Task<EspecialidadDTo> Agregar(EspecialidadDTo modeloDto)
        {
            try
            {
                Especialidad especialidad = new Especialidad()
                {
                    NombreEspecialidad = modeloDto.NombreEspecialidad,
                    Descripcion = modeloDto.Descripcion,
                    Estado = modeloDto.Estado == 1 ? true : false,
                    FechaCreacion =DateTime.Now,
                    FechaActualizacion = DateTime.Now,  

                };
                await _unidadTrabajo.especialidad.Agregar(especialidad);
                await _unidadTrabajo.Guardar();
                if (especialidad.Id == 0) {
                    throw new TaskCanceledException("La especiliada no se pudo crear");
                }
                return _mapper.Map<EspecialidadDTo>(especialidad);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task Actualizar(EspecialidadDTo modeloDto)
        {
            try
            {
                var especialidadDb = await _unidadTrabajo.especialidad.ObtenerPrimero(e => e.Id == modeloDto.Id);
                if (especialidadDb == null) {
                    throw new TaskCanceledException("La especialidad no existe");
                }
                especialidadDb.NombreEspecialidad = modeloDto.NombreEspecialidad;
                especialidadDb.Descripcion = modeloDto.Descripcion;
                especialidadDb.Estado =modeloDto.Estado ==1 ? true:false;
                _unidadTrabajo.especialidad.Actualizar(especialidadDb);
                await _unidadTrabajo.Guardar();

            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public  async Task Remover(int id)
        {
            try
            {
                var especialidadDb = await _unidadTrabajo.especialidad.ObtenerPrimero(e => e.Id == id);
                if (especialidadDb == null)
                {
                    throw new TaskCanceledException("La especialidad no existe");
                }
                _unidadTrabajo.especialidad.Remover(especialidadDb);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
         

        }
        public async Task<IEnumerable<EspecialidadDTo>> ObtenerTodos()
        {
            try
            {

                var lista = await _unidadTrabajo.especialidad.ObtenerTodos(orderBy: e =>e.OrderBy(e =>e.NombreEspecialidad) );
                return _mapper.Map<IEnumerable<EspecialidadDTo>>(lista);


            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}
