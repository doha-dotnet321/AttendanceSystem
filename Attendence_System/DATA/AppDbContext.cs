using Attendence_System.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Attendence_System.DATA
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AttendenceRecord>AttendenceRecords { get; set; }
        public DbSet<Employee>Employees { get; set; }
    }
}
