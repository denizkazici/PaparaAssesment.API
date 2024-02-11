using System.ComponentModel.DataAnnotations;

namespace PaparaAssesment.Service.DTOs.Payments
{
    public class SubscriptionAddRequestDto
    {
        [Required] public decimal Amount { get; set; } = default!;
        public PaymentCategory PaymentCategory { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int ApartmentId { get; set; }
        
    }
}