using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Beneficiary> Beneficiaries { get; set; }
    public DbSet<Plan> Plans { get; set; }
};