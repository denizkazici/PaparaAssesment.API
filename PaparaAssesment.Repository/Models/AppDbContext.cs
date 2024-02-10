using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaparaAssesment.Repository.Models.Apartments;
using PaparaAssesment.Repository.Models.Buildings;
using PaparaAssesment.Repository.Models.Payments;
using PaparaAssesment.Repository.Models.User;


namespace PaparaAssesment.Repository.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext<AppUser, AppRole, Guid>(options)
{
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Building> Buildings { get; set; } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       

        modelBuilder.Entity<Payment>().Property(x => x.Amount).HasPrecision(18, 2);

        base.OnModelCreating(modelBuilder);
    }
}
