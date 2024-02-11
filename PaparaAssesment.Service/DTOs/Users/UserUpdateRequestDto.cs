using System.ComponentModel.DataAnnotations;

namespace PaparaAssesment.Service.DTOs.Users
{
    public class UserUpdateRequestDto
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = default!;

        [Required]
        public string NameSurname { get; set; } = default!;

        [Required]
        public string TCNo { get; set; } = default!;
    }
}