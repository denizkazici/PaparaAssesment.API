﻿using Microsoft.EntityFrameworkCore;

namespace PaparaAssesment.Repository.Models.Apartments;

public class ApartmentRepositoryWithSql(AppDbContext context) : IApartmentRepository
{
    public Apartment Add(Apartment apartment)
    {
        var building = context.Buildings.FirstOrDefault(x => x.Id == apartment.BuildingId);
        building.Apartments.Add(apartment);
        context.Apartments.Add(apartment);
        return apartment;
    }

    public Apartment Update(Apartment apartment)
    {
        context.Apartments.Update(apartment);
        return apartment;
    }

    public Apartment? GetbyId(int Id)
    {
        return context.Apartments.Include(apartment => apartment.Payments).FirstOrDefault(x => x.Id == Id);
        
    }

    public List<Apartment> GetApartments(int BuildingId)
    {
        return context.Apartments.Where(apartment => apartment.BuildingId == BuildingId).ToList();
    }

    public List<Apartment> GetAlls()
    {
        return context.Apartments.Include(apartment => apartment.User).ToList();
    }
}
