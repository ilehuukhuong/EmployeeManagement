using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeManagement.Application.Core;
using EmployeeManagement.Infrastructure.Data;
using MediatR;

namespace EmployeeManagement.Application.Employees
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<EmployeeDto>>>
        {
            public EmployeeParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<EmployeeDto>>>
        {
            private readonly EmployeeContext _context;
            private readonly IMapper _mapper;
            public Handler(EmployeeContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<PagedList<EmployeeDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Employees
                    .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                if (request.Params.Search != null)
                {
                    query = query.Where(x => x.Name.ToLower().Contains(request.Params.Search.ToLower()));
                }
                switch (request.Params.SortBy)
                {
                    default:
                    case "Id":
                        query = request.Params.OrderBy != null && request.Params.OrderBy.ToLower() == "desc" ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id);
                        break;
                    case "EmployeeCode":
                        query = request.Params.OrderBy != null && request.Params.OrderBy.ToLower() == "desc" ? query.OrderByDescending(x => x.EmployeeCode) : query.OrderBy(x => x.EmployeeCode);
                        break;
                    case "Name":
                        query = request.Params.OrderBy != null && request.Params.OrderBy.ToLower() == "desc" ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                        break;
                    case "Position":
                        query = request.Params.OrderBy != null && request.Params.OrderBy.ToLower() == "desc" ? query.OrderByDescending(x => x.Position) : query.OrderBy(x => x.Position);
                        break;
                    case "Age":
                        query = request.Params.OrderBy != null && request.Params.OrderBy.ToLower() == "desc" ? query.OrderByDescending(x => x.Age) : query.OrderBy(x => x.Age);
                        break;
                }

                return Result<PagedList<EmployeeDto>>.Success(
                    await PagedList<EmployeeDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize)
                );
            }
        }
    }
}
