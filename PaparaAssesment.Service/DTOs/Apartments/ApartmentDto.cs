using PaparaAssesment.Repository.Models.Apartments;
using PaparaAssesment.Repository.Models.User;
using PaparaAssesment.Service.DTOs.Users;
namespace PaparaAssesment.Service.DTOs.Apartments;

public class ApartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; } 
    public string ApartmentType { get; set; } = default!;
    public int Floor { get; set; }
    public UserDto? User { get; set; }
    public int BuildingId { get; set; }
}
