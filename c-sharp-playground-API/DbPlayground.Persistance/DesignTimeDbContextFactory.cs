using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DbPlayground
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CalculatorContext>
    {
        public CalculatorContext CreateDbContext(string[] args)
        {
            var connectionString = "Host=postgresql.local.globalx.com.au;Database=calculator;Username=postgres;Password=ThisSureIsAPassword";
            Console.WriteLine(connectionString);
            var dbContextOptions = new DbContextOptionsBuilder<CalculatorContext>().UseNpgsql(connectionString).Options;
            return new CalculatorContext(dbContextOptions, null);
        }
    }
}
