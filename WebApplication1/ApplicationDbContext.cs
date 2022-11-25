using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entidades;

namespace WebApplication1
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DepaInquilino>().HasKey(aux => new { aux.DepaId, aux.InquiId });
        }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Inquilino> Inquilinos { get; set; }
    }
}
