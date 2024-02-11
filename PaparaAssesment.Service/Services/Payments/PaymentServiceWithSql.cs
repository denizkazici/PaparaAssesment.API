using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using PaparaAssesment.Repository.Models.Apartments;
using PaparaAssesment.Repository.Models.Buildings;
using PaparaAssesment.Repository.Models.Payments;
using PaparaAssesment.Repository.Models.User;
using PaparaAssesment.Service.DTOs.Payments;
using PaparaAssesment.Service.DTOs.Shared;
using PaparaAssesment.Service.Extensions;
using PaparaAssesment.Service.UnitOfWorks;

namespace PaparaAssesment.Service.Services.Payments;

public class PaymentServiceWithSql (IPaymentRepository paymentRepository, IUnitOfWork unitOfWork , UserManager<AppUser> userManager, IApartmentRepository apartmentRepository, PaymentHelper helper) : IPaymentService
{
    public ResponseDto<int> AddBills(BillAddRequestDto request)
    {
        
        List<Apartment> apartmentList = apartmentRepository.GetApartments(request.BuildingId);
        if (apartmentList is null || apartmentList.Count == 0) { return ResponseDto<int>.Fail("Building doesn't include any apartment"); }
        decimal apartmentAmount = request.Amount / apartmentList.Count;
        foreach (Apartment apartment in apartmentList)
        {
            var payment = new Payment
            {
                IsPaid = false,
                Amount = apartmentAmount,
                PaymentCategory = request.PaymentCategory,
                Year = request.Year,
                Month = request.Month,
                ApartmentId = apartment.Id,
                buildingId = apartment.BuildingId
            };
            paymentRepository.AddPayments(payment);
            
        }
        unitOfWork.Commit();
        return ResponseDto<int>.Success(request.BuildingId);
    }

    public ResponseDto<int> AddSubscription(SubscriptionAddRequestDto request)
    {
        Apartment? apartment = apartmentRepository.GetbyId(request.ApartmentId);
        if(apartment is null) { return ResponseDto<int>.Fail("Apartment does not found"); }
        var payment = new Payment
        {
            IsPaid = false,
            Amount = request.Amount,
            PaymentCategory = request.PaymentCategory,
            Year = request.Year,
            Month = request.Month,
            ApartmentId = request.ApartmentId
        };
        paymentRepository.AddPayments(payment);
        unitOfWork.Commit();
        return ResponseDto<int>.Success(payment.PaymentId);
    }

    public ResponseDto<int> UpdatePayment(PayPaymentRequestDto request)
    {
        
        Payment? payment = paymentRepository.GetPayment(request.PaymentId);

        if (payment == null) { return ResponseDto<int>.Fail("Payment does not found");}

        if (!helper.CalculateRegularPayingUser(request.UserId))
        {
            payment.Amount = payment.Amount - (payment.Amount * 0.10m);
        }
        if ((payment.Month != DateTime.Now.Month) || (payment.Year != DateTime.Now.Year))  
        { 
            payment.Amount = payment.Amount * 1.10m; 
            payment.IsLate = true;
        }
        payment.PaymentDate = DateTime.Now;
        payment.IsPaid = true;
        payment.PaymentType = request.PaymentType;
        paymentRepository.UpdatePayments(payment);
        unitOfWork.Commit();
        return ResponseDto<int>.Success(payment.PaymentId);
    }
    //Kullanıcının ödenmemiş fatura ve aidatları döner
    public async Task<ResponseDto<List<PaymentDto>>> UserUnpaidPaymentsById(string userId)
    {
        var appUser = await userManager.FindByIdAsync(userId);
        if (appUser is null) { return ResponseDto<List<PaymentDto>>.Fail("User does not found"); }
        AppUser hasUser = appUser;
        if (hasUser.ApartmentId is null) { return ResponseDto<List<PaymentDto>>.Fail("user is not a resident of an apartment"); }
        List<Payment> PaymentList = paymentRepository.GetUserUnpaidPaymentsById((int)hasUser.ApartmentId);
        List<PaymentDto> PaymentListWithDto = PaymentList.ToPaymentListDto(PaymentList);
        return ResponseDto<List<PaymentDto>>.Success(PaymentListWithDto);
    }

    //dairenin tüm paymentsleri
    public ResponseDto<List<PaymentDto>> ApartmentsPayments()
    {
        List<Payment> PaymentList = paymentRepository.GetApartmentsPayments();
        List<PaymentDto> PaymentListWithDto = PaymentList.ToPaymentListDto(PaymentList);

        return ResponseDto<List<PaymentDto>>.Success(PaymentListWithDto);
    }

    //belli bir ay ve yıldaki daire başına ödemeler 
    public ResponseDto<List<PaymentDto>> PaymentsbyMonthYearById(int Year, int Month, int ApartmentId)
    {
        List<Payment> PaymentList = paymentRepository.GetPaymentsbyMonthYearById(Year, Month, ApartmentId);
        List<PaymentDto> PaymentListWithDto = PaymentList.ToPaymentListDto(PaymentList);
        return ResponseDto<List<PaymentDto>>.Success(PaymentListWithDto);
    }

    //binanın tüm ödemeleri
    public ResponseDto<List<PaymentDto>> BuildingsPayments(int BuildingId)
    {
        List<Payment> PaymentList = paymentRepository.GetBuildingsPayments(BuildingId);
        List<PaymentDto> PaymentListWithDto = PaymentList.ToPaymentListDto(PaymentList);
        return ResponseDto<List<PaymentDto>>.Success(PaymentListWithDto);
    }

    //kullanıcı kendi ödemelerini
    public ResponseDto<List<PaymentDto>> UserPaidPaymentsById(string userId)
    {
        var appUser = userManager.Users.First(x=>x.Id.ToString() == userId);

        if (appUser is null) { return ResponseDto<List<PaymentDto>>.Fail("User does not found"); }
        AppUser hasUser = appUser;

        if (hasUser.ApartmentId is null) { return ResponseDto<List<PaymentDto>>.Fail("user is not a resident of an apartment"); }

        List<Payment> PaymentList = paymentRepository.GetUserPaidPaymentsById((int)hasUser.ApartmentId);
        List<PaymentDto> PaymentListWithDto = PaymentList.ToPaymentListDto(PaymentList);
        return ResponseDto<List<PaymentDto>>.Success(PaymentListWithDto);
    }
}
