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

        public async Task<List<OrderDto>> GetOrders(ISender sender)
        {
            return await sender.Send(new GetOrdersQuery());
        }
    }
}
