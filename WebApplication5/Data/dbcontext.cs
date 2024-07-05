using WebApplication5.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Data
{
    public class LeaveRequestContext : DbContext
    {
        public LeaveRequestContext(DbContextOptions<LeaveRequestContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<LeaveRequest> LeaveRequests { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Name = "Alice", Email = "alice@example.com" },
                new Employee { EmployeeId = 2, Name = "Bob", Email = "bob@example.com" },
                new Employee { EmployeeId = 3, Name = "Charlie", Email = "charlie@example.com" }
            );


            modelBuilder.Entity<LeaveRequest>().HasData(
                new LeaveRequest { LeaveRequestId = 1, EmployeeId = 1, StartDate = new DateTime(2024, 7, 1), EndDate = new DateTime(2024, 7, 10), Reason = "Vacation", IsApproved = true, DateNow = new DateTime(2024, 6, 1) },
                new LeaveRequest { LeaveRequestId = 2, EmployeeId = 2, StartDate = new DateTime(2024, 8, 1), EndDate = new DateTime(2024, 8, 5), Reason = "Medical", IsApproved = false, DateNow = new DateTime(2024, 7, 1) },
                new LeaveRequest { LeaveRequestId = 3, EmployeeId = 3, StartDate = new DateTime(2024, 9, 15), EndDate = new DateTime(2024, 9, 20), Reason = "Personal", IsApproved = null, DateNow = new DateTime(2024, 2, 1) }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               
                optionsBuilder.UseSqlServer("Server=DESKTOP-FVF2TLQ;Database=LeaveApp3;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}
