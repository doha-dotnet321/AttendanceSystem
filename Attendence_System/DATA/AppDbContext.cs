using Attendence_System.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Attendence_System.DATA
{
    public class AppDbContext: IdentityDbContext<ApplicationUser> // Inherit from IdentityDbContext<ApplicationUser> to include Identity tables
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AttendenceRecord>AttendenceRecords { get; set; }
        public DbSet<Employee>Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
    }
}
