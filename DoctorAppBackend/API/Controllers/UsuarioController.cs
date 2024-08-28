using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entidades;

namespace API.Controllers
{

    public class UsuarioController : BaseController
    {
        private readonly UserManager<UsuarioAplicacion> _db;
        private readonly ITokenServicio _tokenServicio;
        private ApiResponse _apiResponse;
        private readonly RoleManager<RolAplicacion> _rolManager;
        public UsuarioController(UserManager<UsuarioAplicacion> db,ITokenServicio tokenServicio, ApiResponse apiResponse, RoleManager<RolAplicacion> rolManager)
        {
            _db = db;
            _tokenServicio = tokenServicio;
            _apiResponse = new();
            _rolManager = rolManager;
        }
        /*
         * [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            var usuarios = await _db.Usuarios.ToListAsync();

            return Ok(usuarios);

        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _db.Usuarios.FindAsync(id);

            return Ok(usuario); 

        }
         */
        [Authorize(Policy = "AdminRol")]
        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {

            if(await UsuarioExiste(registroDto.Username)) return BadRequest("UserName ya esta Registrado");

            //using var hmac = new HMACSHA512();
            var usuario = new UsuarioAplicacion
            {
                UserName = registroDto.Username,
                Apellidos = registroDto.Apellidos,
                Nombres = registroDto.Nombres,
                Email = registroDto.Email

             //   passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Password)),
             //   passwordSalt = hmac.Key
            };
            var resultado = await _db.CreateAsync(usuario, registroDto.Password);
            if (!resultado.Succeeded)
            {
                return BadRequest(resultado.Errors.ToString());
            }
            var rolResultado = await _db.AddToRoleAsync(usuario, registroDto.Rol);
            if (!rolResultado.Succeeded)
            {
                return BadRequest(resultado.Errors.ToString());

            }
            return new UsuarioDto
            {
                Username = usuario.UserName,
                Token = await _tokenServicio.CrearToken(usuario)
            };

        }
        private async Task<bool> UsuarioExiste(string Username)
        {
            
            return await _db.Users.AnyAsync(u => u.UserName == Username.ToLower());

        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _db.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (usuario == null) return Unauthorized("Usuario no válido");

          //  using var hmac = new HMACSHA512(usuario.passwordSalt);
           // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            //if (!computedHash.SequenceEqual(usuario.passwordHash))
             //   return Unauthorized("Contraseña no válida");
             var resultado = await _db.CheckPasswordAsync(usuario,loginDto.Password);
            if (!resultado)
            {
                return Unauthorized("password no valido");
            }
            return new UsuarioDto
            {
                Username = usuario.UserName,
                Token = await _tokenServicio.CrearToken(usuario),
            };
        }
        [Authorize(Policy = "AdminRol")]

        [HttpGet("ListadoRoles")]
        public  IActionResult GetRoles()
        {
            var roles = _rolManager.Roles.Select( r => new {NombreRol = r.Name} ).ToList();
            _apiResponse.Resultado = roles;
            _apiResponse.IsExitoso = true;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_apiResponse);


        }

    }
}
