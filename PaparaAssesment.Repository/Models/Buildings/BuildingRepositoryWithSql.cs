using Microsoft.EntityFrameworkCore;
using PaparaAssesment.Repository.Models.Apartments;

namespace PaparaAssesment.Repository.Models.Buildings
{
    public class BuildingRepositoryWithSql(AppDbContext context) : IBuildingRepository
    {
        public Building Add(Building building)
        {
            context.Buildings.Add(building);
            return building;
        }

        public Building? GetById(int id)
        {
            return context.Buildings.FirstOrDefault(x => x.Id == id);
        }
    }
}
