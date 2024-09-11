using API_project.Models;
using Microsoft.EntityFrameworkCore;

namespace API_project.Data;
public class OfficeContext: DbContext
{
    public OfficeContext(DbContextOptions<OfficeContext> options):base(options) 
    {

    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }

}