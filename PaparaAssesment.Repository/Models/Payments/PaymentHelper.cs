using Microsoft.AspNetCore.Identity;
using PaparaAssesment.Repository.Models.User;

namespace PaparaAssesment.Repository.Models.Payments;

public class PaymentHelper (IPaymentRepository paymentRepository, UserManager<AppUser> userManager)
{
    public bool CalculateRegularPayingUser (string id)
    {
        var appUser = userManager.Users.First(x => x.Id.ToString() == id);

        if (appUser is null) { return false; }
        AppUser hasUser = appUser;

        if (hasUser.ApartmentId is null) { return false; }
        int Year = DateTime.Now.Year;
        //var payments = paymentRepository.GetUserYearlyPaids((int)hasUser.ApartmentId, Year);
        for (int i = 1; i < 13; i++)
        {
            var payments = paymentRepository.GetPaymentsbyMonthYearById(Year, i , (int)hasUser.ApartmentId) ;
            if (payments is not null || payments.Count() > 0) {  return false; }
        }
        
        return true;
    }
}

