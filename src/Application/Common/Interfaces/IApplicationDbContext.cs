using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get ; }
    public DbSet<Board> Boards { get ; }
    public DbSet<ListCards> ListsCards{ get ; }
    public DbSet<Card> Cards { get ; }
    public DbSet<Member> Members { get ; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
