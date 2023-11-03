using Microsoft.EntityFrameworkCore;
using SeniorAPI.Models;

namespace SeniorAPI.Data
{
    public class SessionContext : DbContext
    {
        public DbSet<PessoaModel> Pessoas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Server=localhost;" +
                "Port=5432;Database=TESTE;" +
                "User Id=postgres;" +
                "Password=123;");
    }
}
