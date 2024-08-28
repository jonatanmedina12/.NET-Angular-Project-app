using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Data.Servicios
{
    public class TokenServicio : ITokenServicio
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<UsuarioAplicacion> _userManager;
        public TokenServicio(IConfiguration configuration,UserManager<UsuarioAplicacion> userManager)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
            _userManager = userManager; 
        }

        public async Task<string> CrearToken(UsuarioAplicacion usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };
            var roles = await _userManager.GetRolesAsync(usuario);
            claims.AddRange(roles.Select(roles => new Claim(ClaimTypes.Role, roles)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}