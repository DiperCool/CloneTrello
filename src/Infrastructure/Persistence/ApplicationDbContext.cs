using System.Reflection;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext,  IApplicationDbContext
{
    private readonly IDateTime _dateTime;
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,IDateTime dateTime) : base(options)
    {
        _dateTime=dateTime;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Board> Boards => Set<Board>();
    public DbSet<ListCards> ListsCards => Set<ListCards>();
    public DbSet<Card> Cards => Set<Card>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
     public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created =  _dateTime.Now.ToUniversalTime();
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = _dateTime.Now.ToUniversalTime();
                    break;
            }
        }
        var result = await base.SaveChangesAsync(cancellationToken);


        return result;
    }

}
