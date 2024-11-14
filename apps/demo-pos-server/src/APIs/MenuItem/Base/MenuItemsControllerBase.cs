using DemoPos.APIs;
using DemoPos.APIs.Common;
using DemoPos.APIs.Dtos;
using DemoPos.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DemoPos.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MenuItemsControllerBase : ControllerBase
{
    protected readonly IMenuItemsService _service;

    public MenuItemsControllerBase(IMenuItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one MenuItem
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<MenuItem>> CreateMenuItem(MenuItemCreateInput input)
    {
        var menuItem = await _service.CreateMenuItem(input);

        return CreatedAtAction(nameof(MenuItem), new { id = menuItem.Id }, menuItem);
    }

    /// <summary>
    /// Delete one MenuItem
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteMenuItem([FromRoute()] MenuItemWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteMenuItem(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many MenuItems
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<MenuItem>>> MenuItems(
        [FromQuery()] MenuItemFindManyArgs filter
    )
    {
        return Ok(await _service.MenuItems(filter));
    }

    /// <summary>
    /// Meta data about MenuItem records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> MenuItemsMeta(
        [FromQuery()] MenuItemFindManyArgs filter
    )
    {
        return Ok(await _service.MenuItemsMeta(filter));
    }

    /// <summary>
    /// Get one MenuItem
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<MenuItem>> MenuItem(
        [FromRoute()] MenuItemWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.MenuItem(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one MenuItem
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateMenuItem(
        [FromRoute()] MenuItemWhereUniqueInput uniqueId,
        [FromQuery()] MenuItemUpdateInput menuItemUpdateDto
    )
    {
        try
        {
            await _service.UpdateMenuItem(uniqueId, menuItemUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
