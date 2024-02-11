using System.ComponentModel.DataAnnotations;

namespace PaparaAssesment.Service.DTOs.Payments;

public class PayPaymentRequestDto
{
    [Required]
    public string UserId { get; set; } = default!;
    [Required]
    public int PaymentId { get; set; } = default!;
    [Required]
    public string PaymentType { get; set; } = default!;
    

}
