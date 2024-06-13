using System.ComponentModel.DataAnnotations;
using Backend.Controllers;

namespace Backend.Dto
{
    
  public class Autorize
  {
    public string email { get; set; }
    public string password { get; set; }

    public string ?Email { get; set; }
    public string ?Password { get; set; }


  }

}