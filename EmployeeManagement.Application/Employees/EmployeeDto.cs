using EmployeeManagement.Core.Common;

namespace EmployeeManagement.Application.Employees
{
    public class EmployeeDto : EntityBase
    {
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
    }
}
