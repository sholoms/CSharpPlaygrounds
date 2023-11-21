using DbPlayground.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DbPlayground.Persistence;

public class CalculatorContext : DbContext
{

    public DbSet<Equation> Equations { get; set; }

    public CalculatorContext(DbContextOptions options) : base(options)
    {
    }
        
    public CalculatorContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = "Host=localhost; Port=51432; Database=eng; Username=sa; Password=Str0ngPassW0rd";
        options.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Equation>()
            .HasKey(x => x.Id);

        Seed(modelBuilder);
    }
    private void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equation>().HasData(
            new Equation
            {
                Id = 1,
                Calculation = "5+5",
                Result = 10
            });
    }
}