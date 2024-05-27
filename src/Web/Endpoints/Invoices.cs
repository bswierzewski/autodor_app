using Application.Invoices.Commands.CreateInvoice;
using MediatR;
using Web.Infrastructure;

namespace Web.Endpoints
{
    public class Invoices : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .RequireAuthorization()
                .MapPost(CreateInvoice);
        }

        public async Task<IResult> CreateInvoice(ISender sender, CreateInvoiceCommand command)
        {
            await sender.Send(command);

            return Results.NoContent();
        }
    }
}
