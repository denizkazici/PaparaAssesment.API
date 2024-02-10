using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PaparaAssesment.Repository.Models.Apartments;
using PaparaAssesment.Repository.Models.Payments;
using PaparaAssesment.Repository.Models.User;
using PaparaAssesment.Service.DTOs.Apartments;
using PaparaAssesment.Service.DTOs.Payments;
using PaparaAssesment.Service.DTOs.Shared;
using PaparaAssesment.Service.Extensions;
using PaparaAssesment.Service.UnitOfWorks;

namespace PaparaAssesment.Service.Services.Apartments;

public class ApartmentServiceWithSql(IApartmentRepository apartmentRepository,
    IMapper mapper, IUnitOfWork unitOfWork , UserManager<AppUser> userManager) : IApartmentService
     
{
    public ResponseDto<int> CreateApartment(ApartmentAddDtoRequest request)
    {
        var apartment = new Apartment
        {
            Name = request.Name,
            Status = request.Status,
            ApartmentType = request.ApartmentType,
            Floor = request.Floor,
            BuildingId = request.BuildingId
        };
        apartmentRepository.Add(apartment);
        unitOfWork.Commit();
        return ResponseDto<int>.Success(apartment.Id);
    }
   /* public async Task<ResponseDto<int>> AddAsync(ApartmentAddDtoRequest request)
    {
        using var transaction = unitOfWork.BeginTransaction();
        AppUser? appUser = await userManager.FindByIdAsync(request.UserId);
        
        if (appUser is null)
        {
            return ResponseDto<int>.Fail("kullanıcı bulunamadı.");
        }
        AppUser hasUser = appUser;
        
        var apartment = new Apartment
        {
            Name = request.Name,
            Status = request.Status,
            ApartmentType = request.ApartmentType,
            Floor = request.Floor,
            BuildingId = request.BuildingId,
            AppUserId = hasUser.Id,
            User = hasUser
        };
        apartmentRepository.Add(apartment);
        unitOfWork.Commit();

        hasUser.ApartmentId = apartment.Id;
        var result = await userManager.UpdateAsync(hasUser);
        
        transaction.Commit();
        return ResponseDto<int>.Success(apartment.Id);
    }
    */
    public async Task<ResponseDto<int>> AddRelationship(ApartmentRelationshipDtoRequest request)
    {
        
        Apartment? apartment = apartmentRepository.GetbyId(request.ApartmanId);
        if (apartment is null) { return ResponseDto<int>.Fail("kullanıcı bulunamadı."); }

        AppUser? appUser = await userManager.FindByIdAsync(request.UserId);

        if (appUser is null) { return ResponseDto<int>.Fail("kullanıcı bulunamadı."); }
        AppUser hasUser = appUser;

        apartment.AppUserId = hasUser.Id.ToString();
       // apartment.User = hasUser;
        apartmentRepository.Update(apartment);

        hasUser.ApartmentId = apartment.Id;
        await userManager.UpdateAsync(hasUser);

        unitOfWork.Commit();



        return ResponseDto<int>.Success(apartment.Id);
    }

    public ResponseDto<List<ApartmentDto>> GetApartments()
    {
        var ApartmentList = apartmentRepository.GetAlls();
        var ApartmentListWithDto = ApartmentList.ToApartmentListDto(ApartmentList);
        //var ApartmentListWithDto = mapper.Map<List<ApartmentDto>>(ApartmentList);
        return ResponseDto<List<ApartmentDto>>.Success(ApartmentListWithDto);
       
        
        
        
    }
}
