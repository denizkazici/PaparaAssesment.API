using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PaparaAssesment.Service.DTOs.Apartments;
using PaparaAssesment.Service.DTOs.Payments;
using PaparaAssesment.Service.Services.Apartments;
using PaparaAssesment.Service.Services.Payments;

namespace PaparaAssesment.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController (IPaymentService paymentService): ControllerBase
    {
        
        [HttpPost]
        public IActionResult AddSubscription(SubscriptionAddRequestDto request)
        {
            var result = paymentService.AddSubscription(request);
            return Created("", result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddBills(BillAddRequestDto request)
        {
            var result = paymentService.AddBills(request);
            return Created("", result);
        }

        [HttpGet]
        public IActionResult AllApartmentsPayments()
        {
            return Ok(paymentService.ApartmentsPayments());
        }

        [HttpGet("{id}")]
        public IActionResult UserDebtById(string id)
        {
            return Ok(paymentService.UserDebtById(id));
        }

        [HttpGet("{Year}/{Month}/{apartmentId}")]
        public IActionResult PaymentsbyMonthYearById (int Year, int Month, int apartmentId)
        {
            return Ok(paymentService.PaymentsbyMonthYearById(Year, Month, apartmentId));
        }
        [HttpGet("{buildingId}")]
        public IActionResult BuildingPaymentsById(int buildingId)
        {
            return Ok(paymentService.BuildingsPayments(buildingId));
        }

        [Authorize(Roles = "Residance")]
        [HttpGet("{id}")]
        public IActionResult UserPaymentsById(string id)
        {
            return Ok(paymentService.UserPaymentsById(id));
        }

        [Authorize(Roles = "Residance")]
        [HttpPost]
        public IActionResult PayPayment(PayPaymentRequestDto request)
        {
            var result= Ok(paymentService.UpdatePayment(request));
            return Created("", result);
        }

    }
}
