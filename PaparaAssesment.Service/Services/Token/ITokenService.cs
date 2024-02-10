using PaparaAssesment.Service.DTOs.Shared;
using PaparaAssesment.Service.DTOs.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaAssesment.Service.Services.Token;

public interface ITokenService
{
    public Task<ResponseDto<TokenCreateResponseDto>> Create (TokenCreateRequestDto request);
}
