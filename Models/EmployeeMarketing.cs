using System.ComponentModel.DataAnnotations;
using Backend.Controllers;
using Backend.Data;
using Backend.Models;


namespace Backend.Models

{
    public class EmployeeMarketing
    {
      [Key]
      public int id { get; set; }
      public string? name { get; set; }
       public string? email { get; set; }
       public string? password { get; set; }
       public string? role { get; set; }
      public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
  }
    //Este modelo no altera la base de datos, lo que hace esta calse es guardar los tokens y mostrarlos como en el localStorage
  public class Authenticated
  {
    public string? Token {get; set; }

  }

  // Relacion con la Tabla Coupon
 

}