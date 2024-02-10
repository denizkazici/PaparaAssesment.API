using System.ComponentModel.DataAnnotations;

namespace PaparaAssesment.Service.DTOs.Users;

public class RoleCreateRequestDto
{
    [Required]
    public string RoleName { get; set; } = default!;
}