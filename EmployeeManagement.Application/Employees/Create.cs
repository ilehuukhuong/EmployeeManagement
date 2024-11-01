using AutoMapper;
using EmployeeManagement.Application.Core;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Infrastructure.Data;
using FluentValidation;
using MediatR;

namespace EmployeeManagement.Application.Employees
{
    public class Create
    {
        public class Command : IRequest<Result<EmployeeDto>>
        {
            public CreateOrUpdateDto CreateDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CreateDto).SetValidator(new EmployeeValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<EmployeeDto>>
        {
            private readonly EmployeeContext _context;
            private readonly IMapper _mapper;
            public Handler(EmployeeContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<EmployeeDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                request.CreateDto.Id = 0;

                var employee = _mapper.Map<Employee>(request.CreateDto);

                _context.Employees.Add(employee);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<EmployeeDto>.Failure("Failed to create employee");

                return Result<EmployeeDto>.Success(_mapper.Map<EmployeeDto>(employee));
            }
        }
    }
}
