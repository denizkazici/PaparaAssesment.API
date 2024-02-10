using AutoMapper;
using PaparaAssesment.Repository.Models.Apartments;
using PaparaAssesment.Repository.Models.Payments;
using PaparaAssesment.Repository.Models.User;
using PaparaAssesment.Service.DTOs.Apartments;
using PaparaAssesment.Service.DTOs.Payments;
using PaparaAssesment.Service.DTOs.Users;

namespace PaparaAssesment.Service.Mapping;

public class DtoProfile : Profile
{
    public DtoProfile()
    {
        // CreateMap<AppUser, UserDto>();

        CreateMap<Apartment, ApartmentDto>();
        CreateMap<Payment, PaymentDto>();
       

    }
}
