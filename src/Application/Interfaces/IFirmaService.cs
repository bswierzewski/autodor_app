using Application.Invoice.Commands.CreateInvoice;

namespace Application.Common.Interfaces;

public interface IFirmaService
{
    Task<HttpResponseMessage> AddInvoice(InvoiceDto invoice);
}
