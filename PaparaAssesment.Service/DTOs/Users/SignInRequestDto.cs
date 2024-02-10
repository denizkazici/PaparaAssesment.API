
using System.ComponentModel.DataAnnotations;


namespace PaparaAssesment.Service.DTOs.Users;

public class SignInRequestDto
{
    [Required]
    public string TCNo { get; set; }
    [Required]
    public string Password { get; set; }
}
