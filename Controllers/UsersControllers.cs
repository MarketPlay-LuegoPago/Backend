using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class UsersController : Controller
    {
        public readonly BaseContext _context;
        public UsersController(BaseContext context)
        {
            _context = context;
        }
    }
}