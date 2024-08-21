using Application.Invoices.Commands.CreateInvoice;
using Application.Invoices.Commands.DTOs;
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

        public async Task<InvoiceResponseDto> CreateInvoice(ISender sender, CreateInvoiceCommand command)
        {
            return await sender.Send(command);
        }
    }
}
