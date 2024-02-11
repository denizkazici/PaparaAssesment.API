using PaparaAssesment.Repository.Models.Apartments;
using System.ComponentModel.DataAnnotations;


namespace PaparaAssesment.Service.DTOs.Apartments
{
    public class ApartmentAddDtoRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public Status Status { get; set; } 
        [Required] 
        public string ApartmentType { get; set; } = default!;
        public int Floor { get; set; }

        [Required]
        public int BuildingId { get; set; }
    }
}