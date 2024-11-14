using DemoPos.APIs.Common;
using DemoPos.APIs.Dtos;

namespace DemoPos.APIs;

public interface IMenuItemsService
{
    /// <summary>
    /// Create one MenuItem
    /// </summary>
    public Task<MenuItem> CreateMenuItem(MenuItemCreateInput menuitem);

    /// <summary>
    /// Delete one MenuItem
    /// </summary>
    public Task DeleteMenuItem(MenuItemWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many MenuItems
    /// </summary>
    public Task<List<MenuItem>> MenuItems(MenuItemFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about MenuItem records
    /// </summary>
    public Task<MetadataDto> MenuItemsMeta(MenuItemFindManyArgs findManyArgs);

    /// <summary>
    /// Get one MenuItem
    /// </summary>
    public Task<MenuItem> MenuItem(MenuItemWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one MenuItem
    /// </summary>
    public Task UpdateMenuItem(MenuItemWhereUniqueInput uniqueId, MenuItemUpdateInput updateDto);
}
