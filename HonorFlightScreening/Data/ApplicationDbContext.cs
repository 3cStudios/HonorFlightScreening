using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HonorFlightScreening.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<VeteranScreening> VeteranScreenings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important: keep this line

            // Configure VeteranScreening
            modelBuilder.Entity<VeteranScreening>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasColumnType("nvarchar(450)");
                entity.Property(e => e.VeteranName)
                    .HasColumnType("nvarchar(250)");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime2")
                    .IsRequired(false) // Makes it nullable
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime2")
                    .IsRequired(false);
            });
        }
    }
}
