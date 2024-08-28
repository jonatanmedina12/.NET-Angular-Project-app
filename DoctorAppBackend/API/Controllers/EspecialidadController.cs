using BLL.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace API.Controllers
{
    [Authorize(Policy ="AdminAgendandorRol")]
    public class EspecialidadController : BaseController
    {
        private readonly IEspecialidadServicio _especialidadServicio;
        private ApiResponse _response;

        public EspecialidadController(IEspecialidadServicio especialidadServicio)
        {
            _especialidadServicio = especialidadServicio;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Resultado = await _especialidadServicio.ObtenerTodos();
                _response.IsExitoso = true;
                _response.StatusCode = System.Net.HttpStatusCode.OK;

            }
            catch (Exception ex)
            {

                _response.IsExitoso=false;
                _response.Mensaje=ex.Message;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;

            }
            return Ok(_response);

        }
        [HttpGet("listadoActivos")]
        public async Task<IActionResult>ObtenerActivos()
        {
            try
            {
                _response.Resultado = await _especialidadServicio.ObtenerActivos();
                _response.IsExitoso = true;
                _response.StatusCode = System.Net.HttpStatusCode.OK;

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;

            }
            return Ok(_response);

        }
        [HttpPost]
        public async Task<IActionResult>Crear (EspecialidadDTo modeloDto)
        {
            try
            {
                await _especialidadServicio.Agregar(modeloDto);
                _response.IsExitoso = true; 
                _response.StatusCode=System.Net.HttpStatusCode.Created;
            }
            catch (Exception ex)
            {

                _response.IsExitoso=false;
                
                _response.Mensaje = ex.Message;
                _response.StatusCode= System.Net.HttpStatusCode.BadRequest;
            }
            return Ok(_response);   
        }
        [HttpPut]
        public async Task<IActionResult>Editar(EspecialidadDTo modeloDto)
        {
            try
            {
                await _especialidadServicio.Actualizar(modeloDto);
                _response.IsExitoso = true;
                _response.StatusCode=System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                _response.IsExitoso=false;
                _response.Mensaje=ex.Message;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;

            }
            return Ok(_response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult>Eliminar(int id)
        {
            try
            {
                await _especialidadServicio.Remover(id);
                _response.IsExitoso = true;
                _response.StatusCode =System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                _response.IsExitoso=!false;
                _response.Mensaje = ex.Message;
                _response.StatusCode= System.Net.HttpStatusCode.BadRequest;
                throw;
            }
            return BadRequest();

        }

    }
}
