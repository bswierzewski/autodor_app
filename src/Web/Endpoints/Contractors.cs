using Application.Contractors.Queries.GetContractors;
using Domain.Entities;
using MediatR;
using Web.Infrastructure;

namespace Web.Endpoints
{
    public class Contractors : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .RequireAuthorization()
                .MapGet(GetContractors);
        }

        public async Task<IEnumerable<Contractor>> GetContractors(ISender sender)
        {
            return await sender.Send(new GetContractorsQuery());
        }
    }
}
