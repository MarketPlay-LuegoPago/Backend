using System.ComponentModel.DataAnnotations;
using Backend.Controllers;

namespace Backend.Dto
{
    
  public class Autorize
  {
    public string ?email { get; set; }
    public string ?password { get; set; }

  }

}