using PaparaAssesment.Repository.Models.Apartments;
using PaparaAssesment.Service.DTOs.Apartments;
using PaparaAssesment.Service.DTOs.Shared;

namespace PaparaAssesment.Service.Services.Apartments;

public interface IApartmentService
{
    //Task<ResponseDto<int>> AddAsync(ApartmentAddDtoRequest request);
    ResponseDto<int> CreateApartment(ApartmentAddDtoRequest request);
    Task<ResponseDto<int>> AddRelationship(ApartmentRelationshipDtoRequest request);

    ResponseDto<List<ApartmentDto>> GetApartments();
}
