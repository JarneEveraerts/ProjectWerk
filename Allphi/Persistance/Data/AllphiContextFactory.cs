using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistance.Data
{
    public class AllphiContextFactory : IDesignTimeDbContextFactory<AllphiContext>
    {
        public AllphiContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AllphiContext>();
            optionsBuilder.UseSqlServer(@"Data Source=.\sqlexpress;Initial Catalog=Allphi;Integrated Security=True");
            return new AllphiContext(optionsBuilder.Options);
        }
    }
}