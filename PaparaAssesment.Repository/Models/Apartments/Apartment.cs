
using PaparaAssesment.Repository.Models.Buildings;
using PaparaAssesment.Repository.Models.Payments;
using PaparaAssesment.Repository.Models.User;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PaparaAssesment.Repository.Models.Apartments;

public class Apartment
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public Status Status { get; set; } //Dolu Boş
    public string ApartmentType { get; set; } = default!; //2+1,1+1... 
    public int Floor { get; set; }


    [ForeignKey("AppUser")] 
    public string? AppUserId { get; set; }

    public AppUser? User { get; set; }

    [ForeignKey("Building")]
    public int BuildingId { get; set; }
    public Building building { get; set; }

    public List<Payment> Payments { get; set; } = new List<Payment>();


}

public enum Status
{
    Dolu,
    Boş
}
