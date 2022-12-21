using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistance.Data
{
    public class AllphiContextFactory : IDesignTimeDbContextFactory<AllphiContext>
    {
        public AllphiContext CreateDbContext(string[] args)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
            var optionsBuilder = new DbContextOptionsBuilder<AllphiContext>();
            optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=root;database=AllPhi", serverVersion);
            return new AllphiContext(optionsBuilder.Options);
        }
    }
}