using FluentValidation;

namespace EmployeeManagement.Application.Employees
{
    public class EmployeeValidator : AbstractValidator<CreateOrUpdateDto>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.PositionId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.DateOfBirth).NotEmpty();
        }
    }
}