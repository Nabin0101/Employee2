using Data_Access_Layer.Model;
using Microsoft.EntityFrameworkCore;
using Entities.Employee;
using Entities.User;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.ApplicationContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<People> People { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<EmployeeJobHistories> EmployeeJobHistories { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary key types as string
            modelBuilder.Entity<Employee>().Property(e => e.Id);
            modelBuilder.Entity<People>().Property(p => p.Id);
            modelBuilder.Entity<Positions>().Property(p => p.Id);
            modelBuilder.Entity<EmployeeJobHistories>().Property(ej => ej.Id);

            // One-to-One relationship between People and Employee
            modelBuilder.Entity<People>()
                .HasOne(p => p.Employee)
                .WithOne(e => e.People)
                .HasForeignKey<Employee>(e => e.PersonId);

            modelBuilder.Entity<People>()
           .HasIndex(e => e.FirstName)
           .HasDatabaseName("idx_people_firstName");

            // One-to-One relationship between User and Token
            modelBuilder.Entity<User>()
                .HasOne(u => u.Token)
                .WithOne(t => t.User)
                .HasForeignKey<Token>(t => t.UserId);

            // One-to-Many relationship between Employee and EmployeeJobHistories
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeJobHistories)
                .WithOne(ej => ej.Employee)
                .HasForeignKey(ej => ej.EmployeeId);

            // One-to-Many relationship between Positions and EmployeeJobHistories
            modelBuilder.Entity<Positions>()
                .HasMany(p => p.EmployeeJobHistories)
                .WithOne(ej => ej.Position)
                .HasForeignKey(ej => ej.PositionId);

            // Many-to-Many relationship between Employee and Positions
            modelBuilder.Entity<EmployeePosition>()
                .HasKey(ep => new { ep.EmployeeId, ep.PositionId });

            modelBuilder.Entity<EmployeePosition>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeePositions)
                .HasForeignKey(ep => ep.EmployeeId);

            modelBuilder.Entity<EmployeePosition>()
                .HasOne(ep => ep.Position)
                .WithMany(p => p.EmployeePositions)
                .HasForeignKey(ep => ep.PositionId);

            modelBuilder.Entity<Token>().HasKey(t => t.Id);

            base.OnModelCreating(modelBuilder);
        }

    }
}
