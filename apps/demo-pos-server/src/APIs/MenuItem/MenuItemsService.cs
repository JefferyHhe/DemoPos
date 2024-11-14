using DemoPos.Infrastructure;

namespace DemoPos.APIs;

public class MenuItemsService : MenuItemsServiceBase
{
    public MenuItemsService(DemoPosDbContext context)
        : base(context) { }
}
