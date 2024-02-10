using Microsoft.EntityFrameworkCore.Storage;

namespace PaparaAssesment.Service.UnitOfWorks
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();

        IDbContextTransaction BeginTransaction();
    }
}
