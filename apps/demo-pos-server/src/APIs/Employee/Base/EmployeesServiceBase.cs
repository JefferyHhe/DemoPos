using DemoPos.APIs;
using DemoPos.APIs.Common;
using DemoPos.APIs.Dtos;
using DemoPos.APIs.Errors;
using DemoPos.APIs.Extensions;
using DemoPos.Infrastructure;
using DemoPos.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPos.APIs;

public abstract class EmployeesServiceBase : IEmployeesService
{
    protected readonly DemoPosDbContext _context;

    public EmployeesServiceBase(DemoPosDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Employee
    /// </summary>
    public async Task<Employee> CreateEmployee(EmployeeCreateInput createDto)
    {
        var employee = new EmployeeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            employee.Id = createDto.Id;
        }

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<EmployeeDbModel>(employee.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Employee
    /// </summary>
    public async Task DeleteEmployee(EmployeeWhereUniqueInput uniqueId)
    {
        var employee = await _context.Employees.FindAsync(uniqueId.Id);
        if (employee == null)
        {
            throw new NotFoundException();
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Employees
    /// </summary>
    public async Task<List<Employee>> Employees(EmployeeFindManyArgs findManyArgs)
    {
        var employees = await _context
            .Employees.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return employees.ConvertAll(employee => employee.ToDto());
    }

    /// <summary>
    /// Meta data about Employee records
    /// </summary>
    public async Task<MetadataDto> EmployeesMeta(EmployeeFindManyArgs findManyArgs)
    {
        var count = await _context.Employees.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Employee
    /// </summary>
    public async Task<Employee> Employee(EmployeeWhereUniqueInput uniqueId)
    {
        var employees = await this.Employees(
            new EmployeeFindManyArgs { Where = new EmployeeWhereInput { Id = uniqueId.Id } }
        );
        var employee = employees.FirstOrDefault();
        if (employee == null)
        {
            throw new NotFoundException();
        }

        return employee;
    }

    /// <summary>
    /// Update one Employee
    /// </summary>
    public async Task UpdateEmployee(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeUpdateInput updateDto
    )
    {
        var employee = updateDto.ToModel(uniqueId);

        _context.Entry(employee).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Employees.Any(e => e.Id == employee.Id))
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
