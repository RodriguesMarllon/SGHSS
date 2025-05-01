using Domain.Entities.Pacientes;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Configuration;

public class SGHSSContext : DbContext
{
    public SGHSSContext(DbContextOptions<SGHSSContext> options) : base(options)
    {
        this.Database.Migrate();
    }

    public DbSet<Paciente> Pacientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {       
        base.OnModelCreating(modelBuilder);

        DefineForeignKeys(modelBuilder);

        CreateSeedCollectionType(modelBuilder);
    }

    private void DefineForeignKeys(ModelBuilder modelBuilder)
    {
        
    }

    private void CreateSeedCollectionType(ModelBuilder modelBuilder)
    {

    }
}
