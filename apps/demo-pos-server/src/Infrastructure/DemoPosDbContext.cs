using DemoPos.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPos.Infrastructure;

public class DemoPosDbContext : DbContext
{
    public DemoPosDbContext(DbContextOptions<DemoPosDbContext> options)
        : base(options) { }

    public DbSet<MenuItemDbModel> MenuItems { get; set; }

    public DbSet<OrderDbModel> Orders { get; set; }

    public DbSet<EmployeeDbModel> Employees { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }
}
