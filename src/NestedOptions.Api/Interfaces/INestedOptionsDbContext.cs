using NestedOptions.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace NestedOptions.Api.Interfaces
{
    public interface INestedOptionsDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Preferences> Preferences { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
