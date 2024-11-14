using Microsoft.AspNetCore.Mvc;

namespace DemoPos.APIs;

[ApiController()]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
