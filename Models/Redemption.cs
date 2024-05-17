using System.ComponentModel.DataAnnotations;


namespace Backend.Models

{
    public class Redemption
    {
      [Key]
      public int id { get; set; }
      public int buys_id { get; set; }
      public DateTime? fecha_redencion { get; set; }
       public int? EmployeeMarketing_id { get; set; }
       public int? Coupon_id { get; set; }
       public int? user_id { get; set; }
  }

}