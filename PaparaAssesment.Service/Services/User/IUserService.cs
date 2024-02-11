using PaparaAssesment.Service.DTOs.Shared;
using PaparaAssesment.Service.DTOs.Users;

namespace PaparaAssesment.Service.Services.User;

public interface IUserService
{
    Task<ResponseDto<List<UserDto>>> GetReqularPayingUsers();
    Task<ResponseDto<Guid?>> Create(UserAddDtoRequest request);
    Task<ResponseDto<string>> CreateRole(RoleCreateRequestDto request);
    Task<ResponseDto<string>> AssignRole(AssignRoleRequestDto request);
    Task<ResponseDto<string>> Update(UserUpdateRequestDto request);
    Task<ResponseDto<string>> Delete(UserDeleteRequestDto request);

}
