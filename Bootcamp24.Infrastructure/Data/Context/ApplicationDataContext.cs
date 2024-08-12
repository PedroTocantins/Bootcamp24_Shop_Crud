using Bootcamp24.Domain.Entities;
using Bootcamp24.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bootcamp24.Infrastructure.Data.Context;

public class ApplicationDataContext : DbContext, IUnitOfWork
{
    public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
    {

    }

    public DbSet<Product> Product => Set<Product>();
    public DbSet<Category> Category => Set<Category>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
