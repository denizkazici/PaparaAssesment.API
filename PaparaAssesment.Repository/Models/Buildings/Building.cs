using PaparaAssesment.Repository.Models.Apartments;
using PaparaAssesment.Repository.Models.Payments;

namespace PaparaAssesment.Repository.Models.Buildings;

public class Building
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public List<Apartment> Apartments { get; set; } = new List<Apartment>();
    //public List<Payment>? Payments { get; set; }

}
