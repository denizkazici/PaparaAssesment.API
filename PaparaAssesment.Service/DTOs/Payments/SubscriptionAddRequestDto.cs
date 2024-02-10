using PaparaAssesment.Repository.Models.User;

namespace PaparaAssesment.Service.DTOs.Payments
{
    public class SubscriptionAddRequestDto
    {
        public decimal Amount { get; set; } = default!;
        public PaymentCategory PaymentCategory { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        //public string UserId { get; set; }
        public int ApartmentId { get; set; }
        
    }
}