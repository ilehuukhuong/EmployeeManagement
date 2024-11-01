using EmployeeManagement.Core.Common;

namespace EmployeeManagement.Application.Employees
{
    public class CreateOrUpdateDto : EntityBase
    {
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int PositionId { get; set; }
    }
}
