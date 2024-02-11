using System.ComponentModel.DataAnnotations;

namespace PaparaAssesment.Service.DTOs.Users;

public class UserDeleteRequestDto
{
    [Required] 
    public string Email { get; set; } = default!;
}