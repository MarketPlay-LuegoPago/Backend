using System.ComponentModel.DataAnnotations;


namespace Backend.Models

{
    public class coupon_category 
    {
      [Key]
      public int id { get; set; }
      public string? name { get; set; }
      public string? description { get; set; }
      public string? state { get; set; }
      public DateTime? creation_date { get; set; }
      public int? reator_category { get; set; }

  }

}