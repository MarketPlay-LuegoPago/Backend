using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;
using Backend.Data;
using Microsoft.AspNetCore.Authorization; //Este es para el token para

//Este controlador le brinda seguridad al endpoint del login

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
         private readonly BaseContext _context;
      public CustomersController(BaseContext context)
      {
          _context = context;
      }
      [HttpGet, Authorize]
      public IEnumerable<string> Get()
      {
        return new string []
        {
            " Jhon Doe ",
            " Chandrashekhar Singh "
        };
      }
      
    }
}