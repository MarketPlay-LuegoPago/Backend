using System.ComponentModel.DataAnnotations;


namespace Backend.Models

{
    public class CouponCategory
{
    [Key]
    public int id { get; set; }
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    
}

}