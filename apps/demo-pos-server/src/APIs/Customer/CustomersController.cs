using Microsoft.AspNetCore.Mvc;

namespace DemoPos.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
