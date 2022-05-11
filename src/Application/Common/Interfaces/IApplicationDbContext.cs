﻿using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get ; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
