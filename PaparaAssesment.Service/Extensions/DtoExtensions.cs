using PaparaAssesment.Repository.Models.Apartments;
using PaparaAssesment.Repository.Models.Payments;
using PaparaAssesment.Repository.Models.User;
using PaparaAssesment.Service.DTOs.Apartments;
using PaparaAssesment.Service.DTOs.Payments;
using PaparaAssesment.Service.DTOs.Users;
namespace PaparaAssesment.Service.Extensions;

public static class DtoExtensions
{
    public static List<PaymentDto> ToPaymentListDto(this List<Payment> payments, List<Payment> paymentList)
    {
        List<PaymentDto> paymentDtos = payments.Select(x => new PaymentDto
        {
            PaymentId = x.PaymentId,
            Apartment = x.Apartment.ToApartmentDto(x.Apartment),
            IsPaid = x.IsPaid,
            PaymentCategory = x.PaymentCategory,
            Amount = x.Amount,
            Year = x.Year,
            Month = x.Month,
            //buildingId = x.buildingId
        }).ToList();
        return paymentDtos;
    }
    public static List<UserDto> ToUserListDto(this List<AppUser> appUsers, List<AppUser> userList)
    {
        List<UserDto> userDtos = appUsers.Select(x => new UserDto
        {
            Id = x.Id,
            UserName = x.UserName,
            ApartmentId = x.ApartmentId,
        }).ToList();
        return userDtos;
    }
    public static UserDto ToUserDto(this AppUser appUser, AppUser user)
    {
        UserDto userDto = new UserDto
        {
            Id = appUser.Id,
            UserName = appUser.UserName,
            ApartmentId = appUser.ApartmentId,
        };
        return userDto;
    }

    public static List<ApartmentDto> ToApartmentListDto(this List<Apartment> apartmentList, List<Apartment> apartment)
    {
        List<ApartmentDto> apartmentDtoList = apartmentList.Select(x=> new ApartmentDto
        {
            Id = x.Id,
            Name    = x.Name,
            ApartmentType = x.ApartmentType,
            BuildingId = x.BuildingId,
            Floor = x.Floor,
            Status = x.Status,
            User   = x.User.ToUserDto(x.User),
        }).ToList ();
        return apartmentDtoList;
    }
    public static ApartmentDto ToApartmentDto(this Apartment apartment, Apartment apartments)
    {
        ApartmentDto apartmentDto = new ApartmentDto
        {
            Id = apartment.Id,
            Name = apartment.Name,
            ApartmentType = apartment.ApartmentType,
            BuildingId = apartment.BuildingId,
            Floor = apartment.Floor,
            Status = apartment.Status,
            User = apartment.User.ToUserDto(apartment.User),
        };
        return apartmentDto;
    }


}
