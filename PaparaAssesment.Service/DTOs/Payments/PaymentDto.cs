using PaparaAssesment.Repository.Models.Apartments;
using PaparaAssesment.Service.DTOs.Apartments;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaparaAssesment.Service.DTOs.Payments;

public class PaymentDto
{
    public int PaymentId { get; set; }
    
    public bool IsPaid { get; set; }
    public decimal Amount { get; set; } 
    public string? PaymentType { get; set; } // credit card or cash
    public PaymentCategory PaymentCategory { get; set; } 

    public int Year { get; set; }
    public int Month { get; set; }

    public ApartmentDto Apartment { get; set; }
    
    //public int buildingId { get; set; }
}
