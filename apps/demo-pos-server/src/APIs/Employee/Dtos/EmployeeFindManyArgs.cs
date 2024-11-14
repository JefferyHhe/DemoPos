using DemoPos.APIs.Common;
using DemoPos.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoPos.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class EmployeeFindManyArgs : FindManyInput<Employee, EmployeeWhereInput> { }