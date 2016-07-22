using BenchmarkLab.Data.Models;
using BenchmarkLab.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BenchmarkLab.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Benchmark>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.OwnerId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.WhenCreated).HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<BenchmarkTest>(entity =>
            {
                entity.Property(e => e.BenchmarkText).IsRequired();

                entity.Property(e => e.TestName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Benchmark)
                    .WithMany(p => p.BenchmarkTest)
                    .HasForeignKey(d => d.BenchmarkId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_BenchmarkTest_ToBenchmark");
            });
        }
        
        public virtual DbSet<Benchmark> Benchmark { get; set; }
        public virtual DbSet<BenchmarkTest> BenchmarkTest { get; set; }
    }
}
