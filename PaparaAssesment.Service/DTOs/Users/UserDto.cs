using Microsoft.AspNetCore.Identity;

namespace PaparaAssesment.Service.DTOs.Users;

public class UserDto
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public int? ApartmentId { get; set; }


}
