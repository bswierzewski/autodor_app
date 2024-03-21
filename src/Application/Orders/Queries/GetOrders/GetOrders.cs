using Application.Common.Interfaces;
using Application.Interfaces;
using AutoMapper;

namespace Application.Orders.Queries;

public record GetOrdersQuery() : IRequest<List<OrderDto>>;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserSetting _userSetting;

    public GetOrdersQueryHandler(IApplicationDbContext context, IMapper mapper, IUserSetting userSetting)
    {
        _context = context;
        _mapper = mapper;
        _userSetting = userSetting;
    }

    public async Task<List<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var userSetting = _userSetting.GetCurrentUserSetting();

        if (userSetting == null)
            throw new Exception("User doesn't have a settings. Please contact with admin");

        return new List<OrderDto>();
    }
}