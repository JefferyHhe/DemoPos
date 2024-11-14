using DemoPos.APIs;

namespace DemoPos;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IEmployeesService, EmployeesService>();
        services.AddScoped<IMenuItemsService, MenuItemsService>();
        services.AddScoped<IOrdersService, OrdersService>();
    }
}
