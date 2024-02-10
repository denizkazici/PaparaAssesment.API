namespace PaparaAssesment.Repository.Models.Apartments;

public interface IApartmentRepository
{
    Apartment Add(Apartment apartment);
    Apartment Update (Apartment apartment);
    Apartment? GetbyId(int Id);
    List<Apartment> GetApartments(int BuildingId);

    List<Apartment> GetAlls();
}
