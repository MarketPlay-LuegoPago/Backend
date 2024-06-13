using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dto
{
    public class CreateCouponDto
    {
       public string name { get; set; }
        public string description { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime activation_date { get; set; }
        public DateTime expiration_date { get; set; }
        public decimal discount { get; set; }
        public string status { get; set; }
        public string use_type { get; set; }
        public int quantity_uses { get; set; }
        public string discount_type { get; set; }
        public int EmployeeMarketing { get; set; }
        public int categoryId { get; set; }
    }
}