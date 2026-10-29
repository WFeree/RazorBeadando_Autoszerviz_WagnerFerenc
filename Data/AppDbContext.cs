using Microsoft.EntityFrameworkCore;
using RazorBeadando_Autoszerviz_WagnerFerenc.Models;

namespace RazorBeadando_Autoszerviz_WagnerFerenc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Ugyfel> Ugyfelek => Set<Ugyfel>();
        public DbSet<Javitas> Javitasok => Set<Javitas>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Ugyfel>()
                .HasMany(c => c.Javitasok)
                .WithOne(s => s.Ugyfel)
                .HasForeignKey(s => s.UgyfelId)
                .OnDelete(DeleteBehavior.Restrict);

            b.Entity<Javitas>().Property(s => s.Allapot).HasConversion<string>();
        }
    }
}
