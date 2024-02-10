using Microsoft.EntityFrameworkCore.Storage;
using PaparaAssesment.Repository.Models;

namespace PaparaAssesment.Service.UnitOfWorks;

public class UnitOfWork (AppDbContext _context) : IUnitOfWork
{
    

    public int Commit()
    {
        return _context.SaveChanges();
    }

    public Task<int> CommitAsync()
    {
        return _context.SaveChangesAsync();
    }


    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }
}
