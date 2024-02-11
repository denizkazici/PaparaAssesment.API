using PaparaAssesment.Repository.Models.Apartments;

namespace PaparaAssesment.Repository.Models.Payments;

public interface IPaymentRepository
{
    Payment AddPayments(Payment payment);
    void UpdatePayments(Payment payment);

    Payment? GetPayment(int PaymentId);
    List<Payment> GetApartmentsPayments();

    List<Payment> GetUserUnpaidPaymentsById(int ApartmentId);
    List<Payment> GetPaymentsbyMonthYearById(int Year, int Month, int apartmentId);
    List<Payment> GetBuildingsPayments(int BuildingId);

    List<Payment> GetUserPaidPaymentsById(int ApartmentId);

    List<Payment> GetUserYearlyUnpaids(int apartmentId, int Year);
    List<Payment> GetUserYearlyPaids(int apartmentId, int Year);
    List<Payment> GetUserYearlyAllPayments(int apartmentId, int Year);

}
