using System.ComponentModel.DataAnnotations;

namespace PaparaAssesment.Service.DTOs.Users;

public class AssignRoleRequestDto
{
    [Required] 
    public string UserId { get; set; } = default!;
    [Required] 
    public string RoleName { get; set; } = default!;
}
