using Microsoft.EntityFrameworkCore;
using TpfinalBack.Models;

namespace TpfinalBack.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "AQAAAAIAAYagAAAAEIOK6i7zHIBdyAKNaZZhPvM+rccIN1F5d/C2Xql552mBbkoiEfopOi4g4zcFD4Qw2A==",
                    Rol = "Admin"
                });

        }
    }
}
