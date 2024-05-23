using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models

{
    public class Coupon
    {
      [Key]
      public int id { get; set; }
      public string? name { get; set; }
       public string? description { get; set; }
       public DateTime? creation_date { get; set; }
       public DateTime? activation_date{ get; set; }
       public DateTime? expiration_date{ get; set; }
       public decimal? discount{ get; set; }
       public string? Status { get; set; }
       public string? use_type { get; set; }
       public int? Quantity_uses { get; set; }
       public string? discount_type { get; set; }

       public int creator_employee_id { get; set; }
       [ForeignKey("id")]
       public EmployeeMarketing CreatorEmployee { get; set; }
       
       public int CategoryId { get; set; } 
       [ForeignKey("CategoryId")]
       public CouponCategory Category { get; set;}


  }

}