using DemoPos.APIs.Dtos;
using DemoPos.Infrastructure.Models;

namespace DemoPos.APIs.Extensions;

public static class MenuItemsExtensions
{
    public static MenuItem ToDto(this MenuItemDbModel model)
    {
        return new MenuItem
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static MenuItemDbModel ToModel(
        this MenuItemUpdateInput updateDto,
        MenuItemWhereUniqueInput uniqueId
    )
    {
        var menuItem = new MenuItemDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            menuItem.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            menuItem.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return menuItem;
    }
}
