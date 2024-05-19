using System.ComponentModel.DataAnnotations;


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
       public DateTime? redemption_date { get; set; }
       public DateTime? expiration_date{ get; set; }
       public decimal? discount{ get; set; }
       public string? Status { get; set; }
       public string? use_type { get; set; }
       public int? Quantity_uses { get; set; }
       public string? discount_type { get; set; }
       public int creator_employee_id { get; set; }
       public int coupon_category { get; set; }

  }

}