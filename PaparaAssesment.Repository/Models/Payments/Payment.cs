
using PaparaAssesment.Repository.Models.Apartments;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaparaAssesment.Repository.Models.Payments
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; } = default!;
        public bool IsLate { get; set; } = false;
        public decimal Amount { get; set; } = default!;
        public string? PaymentType { get; set; } // credit card or cash
        public PaymentCategory PaymentCategory { get; set; } = default!; // aidat ya da fatura (elektrik-su-doğalgaz)

        public int Year { get; set; }
        public int Month { get; set; }

        [ForeignKey("Apartment")]
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        public int buildingId { get; set; }

    }
}
public enum PaymentCategory
{
    Aidat,
    Elektrik ,
    Su ,
    Doğalgaz 
}

