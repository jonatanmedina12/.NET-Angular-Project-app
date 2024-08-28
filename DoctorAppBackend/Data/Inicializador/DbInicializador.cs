using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Inicializador
{
    public class DbInicializador : IdbInicializador
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UsuarioAplicacion> userManager;
        private readonly RoleManager<RolAplicacion> roleManager;

        public DbInicializador(ApplicationDbContext context, UserManager<UsuarioAplicacion> userManager, RoleManager<RolAplicacion> roleManager)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public  void Inicializar()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0) { 
                    _context.Database.Migrate();

                }
            }
            catch (Exception ex) { 
            
            }
            if(_context.Roles.Any(r=> r.Name == "Admin"))
            {
                return;
            }
             roleManager.CreateAsync(new RolAplicacion { Name = "Admin" }).GetAwaiter().GetResult();

             roleManager.CreateAsync(new RolAplicacion { Name = "Agendandor" }).GetAwaiter().GetResult(); 

             roleManager.CreateAsync(new RolAplicacion { Name = "Doctor" }).GetAwaiter().GetResult();

            var usuario = new UsuarioAplicacion
            {
                UserName = "Administrador",
                Email = "administrador@doctorapp.com",
                Apellidos = "Peidra",
                Nombres = "Calors"
            };
            userManager.CreateAsync(usuario, "Admin123").GetAwaiter().GetResult();

            UsuarioAplicacion usuarioAplicacion = _context.usuarioAplicacions.Where(u => u.UserName == "Administrador").FirstOrDefault();
            userManager.AddToRoleAsync(usuarioAplicacion,"Admin").GetAwaiter().GetResult();


        }
    }
}
