using DemoPos.Infrastructure;

namespace DemoPos.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(DemoPosDbContext context)
        : base(context) { }
}
