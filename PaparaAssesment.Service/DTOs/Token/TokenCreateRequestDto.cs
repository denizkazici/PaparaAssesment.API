namespace PaparaAssesment.Service.DTOs.Token;
public class TokenCreateRequestDto
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}
