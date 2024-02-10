using PaparaAssesment.Service.DTOs.Buildings;
using PaparaAssesment.Service.DTOs.Shared;

namespace PaparaAssesment.Service.Services.Buildings;

public interface IBuildingService
{
    ResponseDto<int> CreateBuilding(BuildingAddDtoRequest request);
}
