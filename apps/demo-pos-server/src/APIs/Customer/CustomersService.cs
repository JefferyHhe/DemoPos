using DemoPos.Infrastructure;

namespace DemoPos.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(DemoPosDbContext context)
        : base(context) { }
}
