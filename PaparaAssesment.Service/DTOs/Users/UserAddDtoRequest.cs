namespace PaparaAssesment.Service.DTOs.Users
{
    public class UserAddDtoRequest
    {
        public string UserName { get; set; } = default!;
        public string NameSurname { get; set; } = default!;
        public string TCNo { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string UserRole {  get; set; } = default!;
    }
}