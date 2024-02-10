using AutoMapper;
using PaparaAssesment.Repository.Models.Apartments;
using PaparaAssesment.Repository.Models.Buildings;
using PaparaAssesment.Service.DTOs.Buildings;
using PaparaAssesment.Service.DTOs.Shared;
using PaparaAssesment.Service.UnitOfWorks;
namespace PaparaAssesment.Service.Services.Buildings;

public class BuildingService(IBuildingRepository buildingRepository, IMapper mapper, IUnitOfWork unitOfWork) : IBuildingService
{
    public ResponseDto<int> CreateBuilding(BuildingAddDtoRequest request)
    {
        var building = new Building
        {
            Name = request.Name,
            
        };
        buildingRepository.Add(building);
        unitOfWork.Commit();
        return ResponseDto<int>.Success(building.Id);
    }
}
