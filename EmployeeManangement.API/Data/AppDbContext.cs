using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManangement.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Tejasvee_Blazor_Employees { get; set; }
        public DbSet<Department> Tejasvee_Blazor_Department { get; set; }
    }
}
