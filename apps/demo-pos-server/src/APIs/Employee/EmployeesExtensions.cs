using DemoPos.APIs.Dtos;
using DemoPos.Infrastructure.Models;

namespace DemoPos.APIs.Extensions;

public static class EmployeesExtensions
{
    public static Employee ToDto(this EmployeeDbModel model)
    {
        return new Employee
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static EmployeeDbModel ToModel(
        this EmployeeUpdateInput updateDto,
        EmployeeWhereUniqueInput uniqueId
    )
    {
        var employee = new EmployeeDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            employee.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            employee.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return employee;
    }
}
