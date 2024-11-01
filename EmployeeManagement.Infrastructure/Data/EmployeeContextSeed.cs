using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Infrastructure.Data
{
    public class EmployeeContextSeed
    {
        public static async Task SeedAsync(EmployeeContext employeeContext)
        {
            // Seed Positions
            if (!employeeContext.Positions.Any())
            {
                employeeContext.Positions.AddRange(GetPositions());
                await employeeContext.SaveChangesAsync();
            }

            // Seed Employees
            if (!employeeContext.Employees.Any())
            {
                employeeContext.Employees.AddRange(GetEmployees(employeeContext));
                await employeeContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Position> GetPositions()
        {
            return new List<Position>
            {
                new() {  Name = "Quản lý" },
                new() {  Name = "Học vụ" },
                new() {  Name = "Tư vấn viên" }
            };
        }

        private static IEnumerable<Employee> GetEmployees(EmployeeContext employeeContext)
        {
            var positions = employeeContext.Positions.ToList();
            var employees = new List<Employee>
            {
                new Employee
                {
                    EmployeeCode = GenerateEmployeeId("Nguyen Van A",DateOnly.FromDateTime(new DateTime(1990, 1, 1)), positions[0].Id),
                    Name = "Nguyen Van A",
                    DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
                    PositionId = positions[0].Id,
                    Position = positions[0]
                },
                new Employee
                {
                    EmployeeCode = GenerateEmployeeId("Tran Thi B",DateOnly.FromDateTime(new DateTime(1992, 5, 12)), positions[1].Id),
                    Name = "Tran Thi B",
                    DateOfBirth = DateOnly.FromDateTime(new DateTime(1992, 5, 12)),
                    PositionId = positions[1].Id,
                    Position = positions[1]
                },
                new Employee
                {
                    EmployeeCode = GenerateEmployeeId("Le Van C", DateOnly.FromDateTime(new DateTime(1995, 3, 30)),positions[2].Id),
                    Name = "Le Van C",
                    DateOfBirth = DateOnly.FromDateTime(new DateTime(1995, 3, 30)),
                    PositionId = positions[2].Id,
                    Position = positions[2]
                }
            };

            // Seed thêm 50 nhân viên
            for (int i = 1; i <= 50; i++)
            {
                string name = $"Employee_{i}";
                DateOnly dob = DateOnly.FromDateTime(new DateTime(1980, 1, 1).AddDays(i * 30));
                var position = positions[i % positions.Count];

                employees.Add(new Employee
                {
                    EmployeeCode = GenerateEmployeeId(name, dob, position.Id),
                    Name = name,
                    DateOfBirth = dob,
                    PositionId = position.Id,
                    Position = position
                });
            }

            return employees;
        }

        private static string GenerateEmployeeId(string fullName, DateOnly dateOfBirth, int position)
        {
            var datePart = dateOfBirth.ToString("yyyy_MM_dd");
            return $"{fullName.Replace(" ", "_")}_{datePart}_{position}";
        }
    }
}
