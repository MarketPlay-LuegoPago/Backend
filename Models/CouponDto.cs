using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class CouponDto
    {
         public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public DateTime? creation_date { get; set; }
        public DateTime? activation_date { get; set; }
        public DateTime? expiration_date { get; set; }
        public decimal? discount { get; set; }
        public string? Status { get; set; }
        public string? use_type { get; set; }
        public int? Quantity_uses { get; set; }
        public string? discount_type { get; set; }
        public string CreatorEmployeeName { get; set; }
        public string CategoryName { get; set; }
    }
}