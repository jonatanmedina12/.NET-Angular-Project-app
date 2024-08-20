using API.Errores;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entidades;

namespace API.Controllers
{
    public class ErrorTestController:BaseController
    {

        private readonly ApplicationDbContext _db;

        public ErrorTestController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("auth")]
        public ActionResult<string> GetNotAuthorize()
        {
            return "No Authorizado";
        }


        [HttpGet("not-found")]
        public ActionResult<Usuario> GetNotFound()
        {

            var objecto = _db.Usuarios.Find(-1);
            if (objecto ==null) return NotFound(new ApiErrorResponse(404));
            return objecto;
        }

     
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {

            var objecto = _db.Usuarios.Find(-1);
            var objectoString = objecto.ToString();

            return objectoString;

           
        }
     
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {


            return BadRequest(new ApiErrorResponse(400));

        }
    }
}
