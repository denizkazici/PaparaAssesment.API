using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaparaAssesment.Repository.Models.User;
using PaparaAssesment.Service.DTOs.Shared;
using PaparaAssesment.Service.DTOs.Users;
using PaparaAssesment.Service.Extensions;
using System.Collections.Generic;

namespace PaparaAssesment.Service.Services.User;

public class UserService (UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager
    ) : IUserService
{
    public async Task<ResponseDto<Guid?>> Create(UserAddDtoRequest request)
    {
        var appUser = new AppUser
        {
            UserName = request.UserName,
            NameSurname = request.NameSurname,
            TCNo = request.TCNo,
            Email = request.Email
        };
        var appRole = new AppRole
        {
            Name = request.UserRole
        };
        var hasRole = await roleManager.RoleExistsAsync(appRole.Name);

        if (!hasRole) { return ResponseDto<Guid?>.Fail("Rol bulunamadı."); }

        var result = await userManager.CreateAsync(appUser, request.Password);

        if (!result.Succeeded)
        {
            var errorList = result.Errors.Select(x => x.Description).ToList();

            return ResponseDto<Guid?>.Fail(errorList);
        }
        var roleAssignResult = await userManager.AddToRoleAsync(appUser, appRole.Name);

        if (!roleAssignResult.Succeeded)
        {
            var errorList = roleAssignResult.Errors.Select(x => x.Description).ToList();

            return ResponseDto<Guid?>.Fail(errorList);
        }
        return ResponseDto<Guid?>.Success(appUser.Id);
    }


    public async Task<ResponseDto<string>> CreateRole(RoleCreateRequestDto request)
    {
        var appRole = new AppRole
        {
            Name = request.RoleName
        };
        var ValidRole = await roleManager.RoleExistsAsync(appRole.Name);
        if (ValidRole)
        {
            return ResponseDto<string>.Fail("Oluşturmak İstediğiniz Rol Bulunuyor");
        }

        var roleCreateResult = await roleManager.CreateAsync(appRole);
        if (roleCreateResult is not null && !roleCreateResult.Succeeded)
        {
            var errorMessages  = roleCreateResult.Errors.Select(x => x.Description).ToList();
            return ResponseDto<string>.Fail(errorMessages);
        }

        // Başarılı yanıt
        return ResponseDto<string>.Success(appRole.Name);
    }
    public async Task<ResponseDto<string>> AssignRole(AssignRoleRequestDto request)
    {
        var appRole = new AppRole
        {
            Name = request.RoleName
        };

        
        var hasRole = await roleManager.RoleExistsAsync(appRole.Name);

        if (!hasRole){ return ResponseDto<string>.Fail("Rol bulunamadı.");}

        var hasUser = await userManager.FindByIdAsync(request.UserId);

        if (hasUser is null){ return ResponseDto<string>.Fail("kullanıcı bulunamadı.");}


        var roleAssignResult = await userManager.AddToRoleAsync(hasUser, appRole.Name);

        if (!roleAssignResult.Succeeded)
        {
            var errorList = roleAssignResult.Errors.Select(x => x.Description).ToList();

            return ResponseDto<string>.Fail(errorList);
        }

        return ResponseDto<string>.Success(string.Empty);
    }

    public async Task<ResponseDto<string>> Delete(UserDeleteRequestDto request)
    {
        var hasUser = await userManager.FindByEmailAsync(request.Email);

        if (hasUser is null)
        {
            return ResponseDto<string>.Fail("kullanıcı bulunamadı.");
        }

        IdentityResult result  = await userManager.DeleteAsync(hasUser);
        if (!result.Succeeded)
        {
            var errorList = result.Errors.Select(x => x.Description).ToList();

            return ResponseDto<string>.Fail(errorList);
        }
        return ResponseDto<string>.Success(string.Empty);
    }

    public async Task<ResponseDto<List<UserDto>>> GetAllUsers()
    {
        var UserList = await userManager.Users.ToListAsync();
        var UserListWithDto = UserList.ToUserListDto(UserList);
        //var UserListWithDto = mapper.Map<List<UserDto>>(UserList);
        return ResponseDto<List<UserDto>>.Success(UserListWithDto);
        
    }

    public ResponseDto<int> Update(UserUpdateRequestDto request)
    {
        throw new NotImplementedException();
    }
}
