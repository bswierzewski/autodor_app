using Application.Common.Interfaces;
using Application.Invoice.Commands.CreateInvoice;
using Application.Orders.Queries;
using AutoMapper;
using Domain.Entities;
using System.Text.Json;

namespace Application.Invoices.Commands.CreateInvoice;

public class CreateInvoiceCommand : IRequest<InvoiceResponseDto>
{
    public int InvoiceNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public DateTime IssueDate { get; set; }
    public IEnumerable<OrderDto> Orders { get; set; }
    public Contractor Contractor { get; set; }
}

public class CreateInvoiceCommandHandler(IMapper mapper,
    IFirmaService firmaService,
    IDistributorsSalesService distributorsSalesService,
    IProductsService productsService) : IRequestHandler<CreateInvoiceCommand, InvoiceResponseDto>
{
    public async Task<InvoiceResponseDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var items = new List<OrderItem>();

        var dates = request.Orders.Select(x => x.Date).Distinct().ToList();

        var orders = new List<Order>();

        foreach (var date in dates)
            orders.AddRange(await distributorsSalesService.GetOrdersAsync(date));

        foreach (var requestOrder in request.Orders)
        {
            var ordersItems = orders
                .Where(order => order.Id == requestOrder.Id)
                .SelectMany(order => order.Items);

            items.AddRange(ordersItems);
        }

        var pozycje = new List<Pozycje>();

        var products = await productsService.GetProductsAsync();

        foreach (var item in items)
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

        var invoice = CreateInvoiceDto(request, pozycje);

        var response = await firmaService.AddInvoice(invoice);

        var responseText = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<InvoiceResponseDto>(responseText);
    }

    private InvoiceDto CreateInvoiceDto(CreateInvoiceCommand request, List<Pozycje> pozycje)
    {
        var kontrahent = mapper.Map<Kontrahent>(request.Contractor);

        return new InvoiceDto()
        {
            Numer = request.InvoiceNumber,
            MiejsceWystawienia = "Leszno",
            TerminPlatnosci = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd"),
            Zaplacono = 0,
            ZaplaconoNaDokumencie = 0,
            LiczOd = "BRT",
            DataWystawienia = request.IssueDate.ToString("yyyy-MM-dd"),
            DataSprzedazy = request.SaleDate.ToString("yyyy-MM-dd"),
            FormatDatySprzedazy = "DZN",
            SposobZaplaty = "PRZ",
            RodzajPodpisuOdbiorcy = "OUP",
            Kontrahent = kontrahent,
            NazwaSeriiNumeracji = "Domyślna roczna",
            Pozycje = pozycje.ToArray(),
        };
    }
}
