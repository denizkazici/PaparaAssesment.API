using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaAssesment.Service.DTOs.Buildings;
using PaparaAssesment.Service.Services.Buildings;

namespace PaparaAssesment.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BuildingController(IBuildingService buildingService) : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateBuilding(BuildingAddDtoRequest request)
        {
            var result = buildingService.CreateBuilding(request);
            return Created("", result);
        }
    }
}
