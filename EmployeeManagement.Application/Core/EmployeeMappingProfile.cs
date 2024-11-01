using AutoMapper;
using EmployeeManagement.Application.Employees;
using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Application.Core
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.Name))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Today.Year - src.DateOfBirth.Year
                    - (DateTime.Today.DayOfYear < src.DateOfBirth.DayOfYear ? 1 : 0)));

            CreateMap<CreateOrUpdateDto, Employee>()
               .ForMember(dest => dest.EmployeeCode, opt => opt.MapFrom(src => GenerateEmployeeId(src.Name, src.DateOfBirth, src.PositionId)));
        }

        private static string GenerateEmployeeId(string fullName, DateOnly dateOfBirth, int position)
        {
            var datePart = dateOfBirth.ToString("yyyy_MM_dd");
            return $"{fullName.Replace(" ", "_")}_{datePart}_{position}";
        }
    }
}
