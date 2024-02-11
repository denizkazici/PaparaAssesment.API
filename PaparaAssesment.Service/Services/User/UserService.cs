using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaparaAssesment.Repository.Models.Payments;
using PaparaAssesment.Repository.Models.User;
using PaparaAssesment.Service.DTOs.Shared;
using PaparaAssesment.Service.DTOs.Users;
using PaparaAssesment.Service.Extensions;

namespace PaparaAssesment.Service.Services.User;

public class UserService (UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        PaymentHelper helper
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

        if (!hasRole) { return ResponseDto<Guid?>.Fail("Role does not found"); }

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
            return ResponseDto<string>.Fail("Role already exist");
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

        if (!hasRole){ return ResponseDto<string>.Fail("Role does not found");}

        var hasUser = await userManager.FindByIdAsync(request.UserId);

        if (hasUser is null){ return ResponseDto<string>.Fail("User does not found");}


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
            return ResponseDto<string>.Fail("User does not found.");
        }

        IdentityResult result  = await userManager.DeleteAsync(hasUser);
        if (!result.Succeeded)
        {
            var errorList = result.Errors.Select(x => x.Description).ToList();

            return ResponseDto<string>.Fail(errorList);
        }
        return ResponseDto<string>.Success(string.Empty);
    }

    public async Task<ResponseDto<List<UserDto>>> GetReqularPayingUsers()
    {
        var UserList = await userManager.Users.ToListAsync();
        List<AppUser> users = new List<AppUser>();
        foreach (AppUser user in UserList)
        {
            if(helper.CalculateRegularPayingUser(user.Id.ToString()))
            {
                users.Add(user);
            }
        }
        var UserListWithDto = users.ToUserListDto(UserList);
        //var UserListWithDto = mapper.Map<List<UserDto>>(UserList);
        return ResponseDto<List<UserDto>>.Success(UserListWithDto);
        
    }

    public async Task<ResponseDto<string>> Update(UserUpdateRequestDto request)
    {
        var user = await userManager.FindByIdAsync(request.Id);
        if (user is null) { return ResponseDto<string>.Fail("user not found"); }

        user.TCNo = request.TCNo;
        user.NameSurname = request.NameSurname;
        
        var result = await userManager.UpdateAsync(user);

        if(!result.Succeeded) { return ResponseDto<string>.Fail("user not updated"); }

        return ResponseDto<string>.Success(""); 

    }
}
