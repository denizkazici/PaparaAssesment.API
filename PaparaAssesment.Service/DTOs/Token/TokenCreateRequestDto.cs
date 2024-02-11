﻿using System.ComponentModel.DataAnnotations;

namespace PaparaAssesment.Service.DTOs.Token;
public class TokenCreateRequestDto
{

    [Required] 
    public string UserName { get; set; } = default!;
    [Required] 
    public string Password { get; set; } = default!;
}
