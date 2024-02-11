using System.ComponentModel.DataAnnotations;
namespace PaparaAssesment.Service.DTOs.Payments
{
    public class BillAddRequestDto
    {
        [Required] 
        public decimal Amount { get; set; } = default!;
        [Required] 
        public PaymentCategory PaymentCategory { get; set; }
        [Required] 
        public int Year { get; set; }
        [Required] 
        public int Month { get; set; }
        [Required] 
        public int BuildingId { get; set; }
    }
}
