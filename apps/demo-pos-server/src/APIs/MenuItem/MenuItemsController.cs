using Microsoft.AspNetCore.Mvc;

namespace DemoPos.APIs;

[ApiController()]
public class MenuItemsController : MenuItemsControllerBase
{
    public MenuItemsController(IMenuItemsService service)
        : base(service) { }
}
