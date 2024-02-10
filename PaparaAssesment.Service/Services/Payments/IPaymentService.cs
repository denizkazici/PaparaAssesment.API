
using PaparaAssesment.Repository.Models.Apartments;
using PaparaAssesment.Repository.Models.Payments;
using PaparaAssesment.Service.DTOs.Payments;
using PaparaAssesment.Service.DTOs.Shared;

namespace PaparaAssesment.Service.Services.Payments;

public interface IPaymentService
{
    ResponseDto<int> AddBills(BillAddRequestDto request);
    ResponseDto<int> AddSubscription(SubscriptionAddRequestDto request);
    ResponseDto<int> UpdatePayment(PayPaymentRequestDto request);
    Task<ResponseDto<List<PaymentDto>>> UserDebtById(string UserId);
    ResponseDto<List<PaymentDto>> ApartmentsPayments();

    ResponseDto<List<PaymentDto>> PaymentsbyMonthYearById(int Year, int Month, int ApartmentId);
    ResponseDto<List<PaymentDto>> BuildingsPayments(int BuildingId);
    ResponseDto<List<PaymentDto>> UserPaymentsById(string userId);
}
