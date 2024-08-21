using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Options;
using Application.Interfaces;
using Application.Invoices.Commands.DTOs;
using Application.Invoices.Extensions;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Application.Invoices.Commands.CreateAllInvoices;

public class CreateAllInvoicesCommand : IRequest<Unit>
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}

public class CreateAllInvoicesCommandHandler(IMapper mapper,
    IFirmaService firmaService,
    IDistributorsSalesService distributorsSalesService,
    IProductsService productsService,
    IContractorService contractorService,
    ISendGridService sendGridService,
    IOptions<SendGridOptions> sendgridOptions,
    IOptions<PolcarOptions> polcarOptions) : IRequestHandler<CreateAllInvoicesCommand, Unit>
{
    public async Task<Unit> Handle(CreateAllInvoicesCommand request, CancellationToken cancellationToken)
    {
        var orders = new List<Order>();
        var responses = new List<InvoiceResponseDto>();

        foreach (var date in DateTimeExtensions.EachDay(request.DateFrom, request.DateTo))
            orders.AddRange(await distributorsSalesService.GetOrdersAsync(date));

        var groupedOrders = orders.GroupBy(x => x.CustomerNumber)
            .ToDictionary(o => o.Key, o => o.ToList());

        var products = await productsService.GetProductsAsync();
        var contrators = await contractorService.GetAsync();

        foreach (var groupedOrder in groupedOrders)
        {
            try
            {
                var pozycje = new List<Pozycje>();

                var items = groupedOrder.Value.SelectMany(x => x.Items);

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
                        TypStawkiVat = "PRC",
                    });
                }

                var contractor = contrators.FirstOrDefault(x => x.NIP == groupedOrder.Key)
                    ?? throw new Exception($"Podany CustomerNumber nie pasuje do żadnego kontrahenta. Zweryfikuj poprawność danych.");

                var invoice = CreateInvoiceDto(contractor, pozycje);

                var response = await firmaService.AddInvoice(invoice);

                var responseText = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<InvoiceResponseDto>(responseText);
                result.CustomerNumber = groupedOrder.Key;

                responses.Add(result);
            }
            catch (Exception ex)
            {
                responses.Add(new InvoiceResponseDto
                {
                    CustomerNumber = groupedOrder.Key,
                    Response = new ResponseDto
                    {
                        Kod = 500,
                        Informacja = ex.Message
                    }
                });
            }
        }

        await sendGridService.SendEmail(sendgridOptions.Value.To, $"Automat {polcarOptions.Value.DistributorCode} | {DateTime.Now}", FormatResponses(responses));

        return await Task.FromResult(Unit.Value);
    }

    private InvoiceDto CreateInvoiceDto(Contractor contractor, List<Pozycje> pozycje)
    {
        var invoice = InvoiceExtensions.CreateDefaultInvoiceDto(pozycje);

        var kontrahent = mapper.Map<Kontrahent>(contractor);

        invoice.Numer = null;
        invoice.DataWystawienia = DateTime.Now.ToString("yyyy-MM-dd");
        invoice.DataSprzedazy = DateTime.Now.ToString("yyyy-MM-dd");
        invoice.Kontrahent = kontrahent;

        return invoice;
    }

    private string FormatResponses(List<InvoiceResponseDto> responses)
    {
        if (responses.Count == 0)
            return "Brak faktur do wystawienia";

        var sb = new StringBuilder();

        var correctResponses = responses.Where(x => x.Response.Kod == 0).ToList();
        var errorResponses = responses.Where(x => x.Response.Kod > 0).ToList();

        if (correctResponses.Count > 0)
        {
            sb.Append("<strong>Poprawinie wystawiono faktury dla:</strong>");
            foreach (var response in correctResponses)
                sb.Append($"<br>{response}");
        }

        if (errorResponses.Count > 0)
        {
            sb.Append("<strong>Wykryto błąd dla:</strong>");
            foreach (var response in errorResponses)
                sb.Append($"<br>{response}");
        }

        return sb.ToString();
    }

}
