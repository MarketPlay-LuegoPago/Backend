using System.ComponentModel.DataAnnotations;


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
  }

}