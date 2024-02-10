using System.ComponentModel.DataAnnotations;

namespace PaparaAssesment.Service.DTOs.Buildings
{
    public class BuildingAddDtoRequest
    {
        [Required]
        public string Name { get; set; } 

    }
}