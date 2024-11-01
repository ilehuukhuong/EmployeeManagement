using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeManagement.Application.Core;
using EmployeeManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Application.Employees
{
    public class Details
    {
        public class Query : IRequest<Result<EmployeeDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<EmployeeDto>>
        {
            private readonly EmployeeContext _context;
            private readonly IMapper _mapper;

            public Handler(EmployeeContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<EmployeeDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var employee = await _context.Employees
                    .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<EmployeeDto>.Success(_mapper.Map<EmployeeDto>(employee));
            }
        }
    }
}
