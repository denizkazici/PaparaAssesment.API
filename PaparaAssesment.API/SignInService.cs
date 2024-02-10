using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PaparaAssesment.Repository.Models.User;
using PaparaAssesment.Service.DTOs.Shared;
using PaparaAssesment.Service.DTOs.Token;
using PaparaAssesment.Service.DTOs.Users;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace PaparaAssesment.API;

public class SignInService (SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IConfiguration configuration)
{
    public async Task<ResponseDto<TokenCreateResponseDto>> SignIn(SignInRequestDto request)
    {
        AppUser hasUser = userManager.Users.First(user => user.TCNo == request.TCNo);
        if(hasUser is null ) { return ResponseDto<TokenCreateResponseDto>.Fail("TCNo or password is wrong"); }
        var checkPassword = await userManager.CheckPasswordAsync(hasUser, request.Password);
        if (checkPassword == false) { return ResponseDto<TokenCreateResponseDto>.Fail("TCNo or password is wrong");}
        var result = await signInManager.PasswordSignInAsync(hasUser, request.Password, isPersistent: true, lockoutOnFailure: false);
        if(!result.Succeeded) { return ResponseDto<TokenCreateResponseDto>.Fail("Giriş Yapılamadı"); }



        //sign in and token return 
        var signatureKey = configuration.GetSection("TokenOptions")["SignatureKey"]!;
        var tokenExpireAsHour = configuration.GetSection("TokenOptions")["Expire"]!;
        var issuer = configuration.GetSection("TokenOptions")["Issuer"]!;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey));

        //payload => list claim Data(Key-value)
        SigningCredentials signingCredentials =
            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claimList = new List<Claim>();

        var userIdAsClaim = new Claim(ClaimTypes.NameIdentifier, hasUser.Id.ToString());
        var userNameAsClaim = new Claim(ClaimTypes.Name, hasUser.UserName!);
        var idAsClaim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());


        var userClaims = await userManager.GetClaimsAsync(hasUser);

        foreach (var claim in userClaims)
        {
            claimList.Add(new Claim(claim.Type, claim.Value));
        }


        claimList.Add(userIdAsClaim);
        claimList.Add(userNameAsClaim);
        claimList.Add(idAsClaim);
        foreach (var role in await userManager.GetRolesAsync(hasUser))
        {
            claimList.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(Convert.ToDouble(tokenExpireAsHour)),
            signingCredentials: signingCredentials,
            claims: claimList,
            issuer: issuer
        );

        var responseDto = new TokenCreateResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
        };


        return ResponseDto<TokenCreateResponseDto>.Success(responseDto);
        //return ResponseDto<string>.Success("");
    }
}
