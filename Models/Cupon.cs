using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Cupon
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActivacion { get; set; }

        public DateTime FechaUso { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public double PorcentajeDescuento { get; set; }

    }
}