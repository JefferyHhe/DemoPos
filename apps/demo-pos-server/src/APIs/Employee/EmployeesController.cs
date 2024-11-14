using Microsoft.AspNetCore.Mvc;

namespace DemoPos.APIs;

[ApiController()]
public class EmployeesController : EmployeesControllerBase
{
    public EmployeesController(IEmployeesService service)
        : base(service) { }
}
