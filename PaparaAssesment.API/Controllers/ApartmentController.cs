using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaparaAssesment.Service.DTOs.Apartments;
using PaparaAssesment.Service.Services.Apartments;

namespace PaparaAssesment.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApartmentController (IApartmentService apartmentService) : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateApartment(ApartmentAddDtoRequest request)
        {
            var result =apartmentService.CreateApartment(request);
            return Created("", result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRelationship(ApartmentRelationshipDtoRequest request)
        {
            var response = await apartmentService.AddRelationship(request);
            if (response.AnyError)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetApartments()
        {
            var response = apartmentService.GetApartments();
            if (response.AnyError)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

    }
}
