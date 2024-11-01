using EmployeeManagement.Core.Common;

namespace EmployeeManagement.Core.Entities
{
    public class Employee : EntityBase
    {
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
