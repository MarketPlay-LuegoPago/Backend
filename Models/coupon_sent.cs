using System.ComponentModel.DataAnnotations;


namespace Backend.Models

{
    public class coupon_sent
    {
      [Key]
      public int id { get; set; }
      public int? user_id { get; set; }
      public int? coupon_id { get; set; }

  }

}