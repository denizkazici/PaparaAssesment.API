using System.ComponentModel.DataAnnotations;

namespace PaparaAssesment.Service.DTOs.Apartments;

public class ApartmentRelationshipDtoRequest
{
    [Required] 
    public int ApartmanId { get; set; }
    [Required]
    public string UserId { get; set; } = default!;
}