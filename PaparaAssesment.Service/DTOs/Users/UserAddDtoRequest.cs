using System.ComponentModel.DataAnnotations;

namespace PaparaAssesment.Service.DTOs.Users
{
    public class UserAddDtoRequest
    {
        [Required]
        public string UserName { get; set; } = default!;
        [Required]
        public string NameSurname { get; set; } = default!;
        [Required]
        public string TCNo { get; set; } = default!;
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
        [Required] 
        public string UserRole {  get; set; } = default!;
    }
}