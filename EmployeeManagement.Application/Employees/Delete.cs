using EmployeeManagement.Application.Core;
using EmployeeManagement.Infrastructure.Data;
using MediatR;

namespace EmployeeManagement.Application.Employees
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly EmployeeContext _context;

            public Handler(EmployeeContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var employee = await _context.Employees.FindAsync(request.Id);

                if (employee == null) return null;

                _context.Remove(employee);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete employee");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
