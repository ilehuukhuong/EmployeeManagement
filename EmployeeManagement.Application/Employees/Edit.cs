using AutoMapper;
using EmployeeManagement.Application.Core;
using EmployeeManagement.Infrastructure.Data;
using FluentValidation;
using MediatR;

namespace EmployeeManagement.Application.Employees
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CreateOrUpdateDto UdpadeDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UdpadeDto).SetValidator(new EmployeeValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly EmployeeContext _context;
            private readonly IMapper _mapper;

            public Handler(EmployeeContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var employee = await _context.Employees.FindAsync(request.UdpadeDto.Id);

                if (employee == null) return null;

                _mapper.Map(request.UdpadeDto, employee);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update employee");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
