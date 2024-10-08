﻿using API.Errores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{ 
    [Route("errores/{codigo}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : BaseController
    {
        public IActionResult Error(int codigo)
        {
            return new ObjectResult(new ApiErrorResponse(codigo));
        }
    }
}
