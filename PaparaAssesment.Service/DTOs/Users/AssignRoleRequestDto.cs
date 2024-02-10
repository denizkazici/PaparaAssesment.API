namespace PaparaAssesment.Service.DTOs.Users;

public class AssignRoleRequestDto
{
    public string UserId { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}
