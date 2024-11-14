using DemoPos.Infrastructure;

namespace DemoPos.APIs;

public class EmployeesService : EmployeesServiceBase
{
    public EmployeesService(DemoPosDbContext context)
        : base(context) { }
}
