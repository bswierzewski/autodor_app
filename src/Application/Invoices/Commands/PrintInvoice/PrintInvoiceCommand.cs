using Application.Common.Interfaces;
using Application.Interfaces;
using Application.Invoices.Commands.DTOs;

namespace Application.Invoices.Commands.PrintInvoice;

public class PrintInvoiceCommand : IRequest<FileInvoiceResponseDto>
{
    public string OrderId { get; set; }
    public DateTime Date { get; set; }
}

public class CreateInvoiceCommandHandler(IPrintService printService,
    IDistributorsSalesService distributorsSalesService,
    IContractorService contractorService,
    IProductsService productsService) : IRequestHandler<PrintInvoiceCommand, FileInvoiceResponseDto>
{
    public async Task<FileInvoiceResponseDto> Handle(PrintInvoiceCommand request, CancellationToken cancellationToken)
    {
        var orders = await distributorsSalesService.GetOrdersAsync(request.Date);

        var order = orders.Where(x => x.Id == request.OrderId).FirstOrDefault()
            ?? throw new Exception($"Nie znaleziono zamówienia o id: {request.OrderId}");

        var products = await productsService.GetProductsAsync();

        var pozycje = new List<Pozycje>();

        foreach (var item in order.Items)
        {
            if (item.TotalPrice <= 0)
                continue;

            var existsName = products.ContainsKey(item?.PartNumber ?? "");

            pozycje.Add(new Pozycje
            {
                Ilosc = item.Quantity,
                CenaJednostkowa = (float)Math.Round(item.TotalPrice * 1.23M, 2),
                Jednostka = "sztuk",
                NazwaPelna = $"{(existsName ? products[item.PartNumber].Name : item.PartNumber)}{(existsName ? $" ({item.PartNumber})" : "")}",
                StawkaVat = 0.23M,
                TypStawkiVat = "PRC"
            });
        }

        var contractors = await contractorService.GetAsync();

        var contractor = contractors.Where(x => x.NIP == order.CustomerNumber).FirstOrDefault();

        // Return the file as a downloadable response
        return new FileInvoiceResponseDto
        {
            FileName = $"{order.Number}.pdf",
            ContentType = "application/pdf",
            Content = printService.Print(contractor, order, pozycje)
        };
    }
}
