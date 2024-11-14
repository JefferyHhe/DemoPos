using DemoPos.APIs;
using DemoPos.APIs.Common;
using DemoPos.APIs.Dtos;
using DemoPos.APIs.Errors;
using DemoPos.APIs.Extensions;
using DemoPos.Infrastructure;
using DemoPos.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPos.APIs;

public abstract class MenuItemsServiceBase : IMenuItemsService
{
    protected readonly DemoPosDbContext _context;

    public MenuItemsServiceBase(DemoPosDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one MenuItem
    /// </summary>
    public async Task<MenuItem> CreateMenuItem(MenuItemCreateInput createDto)
    {
        var menuItem = new MenuItemDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            menuItem.Id = createDto.Id;
        }

        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<MenuItemDbModel>(menuItem.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one MenuItem
    /// </summary>
    public async Task DeleteMenuItem(MenuItemWhereUniqueInput uniqueId)
    {
        var menuItem = await _context.MenuItems.FindAsync(uniqueId.Id);
        if (menuItem == null)
        {
            throw new NotFoundException();
        }

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many MenuItems
    /// </summary>
    public async Task<List<MenuItem>> MenuItems(MenuItemFindManyArgs findManyArgs)
    {
        var menuItems = await _context
            .MenuItems.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return menuItems.ConvertAll(menuItem => menuItem.ToDto());
    }

    /// <summary>
    /// Meta data about MenuItem records
    /// </summary>
    public async Task<MetadataDto> MenuItemsMeta(MenuItemFindManyArgs findManyArgs)
    {
        var count = await _context.MenuItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one MenuItem
    /// </summary>
    public async Task<MenuItem> MenuItem(MenuItemWhereUniqueInput uniqueId)
    {
        var menuItems = await this.MenuItems(
            new MenuItemFindManyArgs { Where = new MenuItemWhereInput { Id = uniqueId.Id } }
        );
        var menuItem = menuItems.FirstOrDefault();
        if (menuItem == null)
        {
            throw new NotFoundException();
        }

        return menuItem;
    }

    /// <summary>
    /// Update one MenuItem
    /// </summary>
    public async Task UpdateMenuItem(
        MenuItemWhereUniqueInput uniqueId,
        MenuItemUpdateInput updateDto
    )
    {
        var menuItem = updateDto.ToModel(uniqueId);

        _context.Entry(menuItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.MenuItems.Any(e => e.Id == menuItem.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
