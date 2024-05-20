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
      [HttpPost ("Login")]
      public async Task <IActionResult> Login([FromBody] EmployeeMarketing employee)
      {
        var EmployeeMarketing = await _context.EmployeeMarketing.FirstOrDefaultAsync(e=> e.email == employee.email && e.password == employee.password); //Con esta variable trato de instanciar la tabla deEmployeeMarketing
        
        if (EmployeeMarketing == null)
        {
            return BadRequest("Error en Correo o Contraseña"); //Mensaje que vera el empleado al ingresar informacio al empleado
        }
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ncjdncjvurbuedxwn233nnedxee+dfr-")); //Llamamos la contraseña del program
        var singninCredentials = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            issuer: "https://localhost:5205",
            audience: "https://localhost:5205",
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMonths(1), // Le decimos al sistema que la session expirara en un mes contando desde que el usuario inicie sesion
            signingCredentials: singninCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return Ok(new Authenticated {Token = tokenString}); //Llamamos la clase que nos guarda el token
      }
      
    }
}