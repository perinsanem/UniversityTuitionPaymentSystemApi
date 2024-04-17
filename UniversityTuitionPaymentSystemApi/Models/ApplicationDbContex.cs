using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace UniversityTuitionPaymentSystemApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Tuition> Tuitions { get; set; }
        public DbSet<Payment> Payments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Tuition>()
                .HasOne(t => t.Student)
                .WithMany(s => s.Tuitions)
                .HasForeignKey(t => t.StudentId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Student)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade); 

            
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, StudentNumber = "S12345" },
                new Student { Id = 2, StudentNumber = "S67890" }
            );

            modelBuilder.Entity<Tuition>().HasData(
                new Tuition { Id = 1, StudentId = 1, Term = "1", Amount = 2000 },
                new Tuition { Id = 2, StudentId = 2, Term = "1", Amount = 4000 }
            );

            modelBuilder.Entity<Payment>().HasData(
                new Payment { Id = 1, StudentId = 1, Term = "1", Amount = 2000, PaymentDate = DateTime.UtcNow },
                new Payment { Id = 2, StudentId = 2, Term = "1", Amount = 2000, PaymentDate = DateTime.UtcNow }
            );
        }
    }

}
