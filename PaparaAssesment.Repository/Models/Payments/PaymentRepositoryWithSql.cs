


using Microsoft.EntityFrameworkCore;

namespace PaparaAssesment.Repository.Models.Payments
{
    public class PaymentRepositoryWithSql(AppDbContext _context) : IPaymentRepository
    {
        public Payment AddPayments(Payment payment)
        {
            _context.Payments.Add(payment);
            return payment;
        }

        public void UpdatePayments(Payment payment)
        {
            _context.Payments.Update(payment);
        }
        public Payment? GetPayment(int PaymentId)
        {
            return _context.Payments.FirstOrDefault(x => x.PaymentId == PaymentId);
        }
        //  Hangi kullanıcınn ne kadar ödemesi olduğunu tek tek görebilir.
        public List<Payment> GetUserDebtById(int ApartmentId)
        {
            List<Payment> result =_context.Payments.Include(x => x.Apartment).Where(p=> p.ApartmentId == ApartmentId).Where(p=> p.IsPaid == false).ToList();
            return result;
        }

        //Daire’lerin yapmış olduğu ödemeleri görebilir
        public List<Payment> GetApartmentsPayments()
        {
            return _context.Payments.Include(x => x.Apartment).ThenInclude(x=> x.User).ToList();
        }

       // Aylık ve Yıllık olarak Daire başına borç durumunu görebilir.
        public List<Payment> GetPaymentsbyMonthYearById(int Year, int Month, int apartmentId)
        {
            return _context.Payments.Include(x => x.Apartment).ThenInclude(x => x.User).Where(x => x.Year == Year && x.Month == Month && x.ApartmentId == apartmentId).ToList();
        }
         //Binanın tüm paymentsleri
        public List<Payment> GetBuildingsPayments(int BuildingId)
        {
            return _context.Payments.Include(x => x.Apartment).ThenInclude(x => x.User).Where(x=>x.buildingId == BuildingId).ToList();
        }
        

        

        public List<Payment>? GetUserYearlyUnpaids(int apartmentId, int Year , int Month)
        {
            return _context.Payments.Include(x => x.Apartment).ThenInclude(x => x.User).Where(x => x.Year == Year && x.Month == Month && x.ApartmentId == apartmentId && x.IsPaid == false).ToList();
           
        }

        //user kendisine ait fatura ve aidat görür
        public List<Payment> GetUserPaymentsById(int ApartmentId)
        {
            List<Payment> result = _context.Payments.Include(x => x.Apartment).Where(p => p.ApartmentId == ApartmentId).ToList();
            return result;
        }
    }
}
