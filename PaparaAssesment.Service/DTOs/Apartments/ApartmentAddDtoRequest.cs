using PaparaAssesment.Repository.Models.Apartments;
using System.ComponentModel.DataAnnotations;


namespace PaparaAssesment.Service.DTOs.Apartments
{
    public class ApartmentAddDtoRequest
    {
        public string Name { get; set; } = default!;

        [Required]
        public Status Status { get; set; } 
        [Required] 
        public string ApartmentType { get; set; } = default!;
        public int Floor { get; set; }

        [Required]
        public int BuildingId { get; set; }
        //public string UserId { get; set; }
    }
}