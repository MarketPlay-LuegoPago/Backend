using System.ComponentModel.DataAnnotations;


namespace Backend.Models

{
    public class Users
    {
      [Key]
      public int id { get; set; }
      public string? name { get; set; }
      public string? lastname { get; set; }
       public string? document_type { get; set; }
       public int? document_number { get; set; }
  }

}