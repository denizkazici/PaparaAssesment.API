using Microsoft.AspNetCore.Identity;
using PaparaAssesment.Repository.Models.Payments;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaparaAssesment.Repository.Models.User;

public class AppUser : IdentityUser<Guid>
{
    public string? NameSurname { get; set; } 
    public string TCNo { get; set; } = default!;

    //public List<Payment>? Payments { get; set; } = new List<Payment>();

    [ForeignKey("Apartment")]
    public int? ApartmentId { get; set; }


}