using Application.Orders.Queries;
using MediatR;
using Web.Infrastructure;

namespace Web.Endpoints
{
    public class Orders : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .RequireAuthorization()
                .MapGet(GetOrders);
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(ISender sender, DateTime dateFrom, DateTime dateTo)
        {
            return await sender.Send(new GetOrdersQuery
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            });
        }
    }
}
