using Application.Common.Extensions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Orders.Queries;

public record GetOrdersQuery() : IRequest<IEnumerable<OrderDto>>
{
    public DateTime DateFrom { get; init; }
    public DateTime DateTo { get; init; }
}

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IProductsService _productsService;
    private readonly IDistributorsSalesService _distributorsSalesService;

    public GetOrdersQueryHandler(IApplicationDbContext context, 
        IMapper mapper, 
        IProductsService productsService, 
        IDistributorsSalesService distributorsSalesService)
    {
        _context = context;
        _mapper = mapper;
        _productsService = productsService;
        _distributorsSalesService = distributorsSalesService;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = new List<Order>();

        foreach (var date in DateTimeExtensions.EachDay(request.DateFrom, request.DateTo))
        {
            orders.AddRange(await _distributorsSalesService.GetOrdersAsync(date));
        }

        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }
}