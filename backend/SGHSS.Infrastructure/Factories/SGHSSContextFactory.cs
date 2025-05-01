using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Infrastructure.Configuration;

namespace Infrastructure.Factories
{
    public class SGHSSContextFactory : IDesignTimeDbContextFactory<SGHSSContext>
    {
        public SGHSSContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SGHSSContext>();
            
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SGHSS;Integrated Security=True;Encrypt=False;");

            return new SGHSSContext(optionsBuilder.Options);
        }
    }
}
