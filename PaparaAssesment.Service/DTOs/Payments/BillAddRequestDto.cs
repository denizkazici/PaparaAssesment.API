using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaAssesment.Service.DTOs.Payments
{
    public class BillAddRequestDto
    {
        public decimal Amount { get; set; } = default!;
        public PaymentCategory PaymentCategory { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        //public string UserId { get; set; }
        //public int ApartmentId { get; set; }
        public int BuildingId { get; set; }
    }
}
