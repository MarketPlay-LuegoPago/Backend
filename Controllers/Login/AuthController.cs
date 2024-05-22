using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Backend.Models;
using Backend.Data;
using Backend.Dto;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BaseContext _context;
      
        public AuthController(BaseContext context)
        {
            _context = context;
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Autorize token)
        {
            var EmployeeMarketing = await _context.EmployeeMarketing.FirstOrDefaultAsync(t => t.email == token.email && t.password == token.password);
            
            if (EmployeeMarketing == null)
            {
                return BadRequest("Error en Correo o Contraseña");
            }
            else{
                
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ncjdncjvurbuedxwn233nnedxee+dfr-"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            
             var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier, EmployeeMarketing.id.ToString()) // Agregar el ID del usuario como reclamación en el token
            }; 
            
            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:5205",
                audience: "https://localhost:5205",
                claims: claims ,// Pasar las reclamaciones al token
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: signingCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
             return Ok(new Authenticated { Token = tokenString });
            }
            
            return Unauthorized();
            
           
        }
    }
}
