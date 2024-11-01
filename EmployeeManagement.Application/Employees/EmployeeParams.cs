using EmployeeManagement.Application.Core;

namespace EmployeeManagement.Application.Employees
{
    public class EmployeeParams : PagingParams
    {
        public string SortBy { get; set; } = "Id";
        public string? OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
