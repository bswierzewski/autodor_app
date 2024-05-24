using Application.Contractors.Commands.CreateContractor;
using Application.Contractors.Commands.DeleteContractor;
using Application.Contractors.Queries.GetContractors;
using Application.Settings.IFirmaSettings.Commands;
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
                .MapGet(GetContractors)
                .MapPost(CreateContractor)
                .MapDelete(DeleteContractor, "{id}");
        }

        public async Task<IEnumerable<Contractor>> GetContractors(ISender sender)
        {
            return await sender.Send(new GetContractorsQuery());
        }

        public async Task<IResult> CreateContractor(ISender sender, CreateContractorCommand command)
        {
            await sender.Send(command);

            return Results.NoContent();
        }

        public async Task<IResult> DeleteContractor(ISender sender, string id)
        {
            await sender.Send(new DeleteContractorCommand(id));

            return Results.NoContent();
        }
    }
}
