using Application.Interfaces;
using Application.Invoices.Commands.DTOs;
using Domain.Entities;
using iText.Html2pdf;

namespace Infrastructure.Services.Printer;

public class PdfPrintService() : IPrintService
{
    public byte[] Print(Contractor contractor, Order order, List<Pozycje> items)
    {
        // Start creating the HTML content
        string htmlContent = $@"
        <html>
            <head>
                <meta charset='utf-8' />
                <title>WZ (Wydanie Zewnętrzne)</title>
                <style>
                    body {{ font-family: Arial, sans-serif; }}
                    h1 {{ text-align: center; }}
                    table {{ width: 100%; border-collapse: collapse; margin-top: 20px; }}
                    th, td {{ border: 1px solid #000; padding: 8px; text-align: left; }}
                    th {{ background-color: #f2f2f2; }}
                    .header {{ margin-bottom: 20px; }}
                </style>
            </head>
            <body>
                <div class='header'>
                    <h1>Wydanie Zewnętrzne</h1>
                    <p><strong>Data:</strong> {order.Date:dd-MM-yyyy}</p>
                    <p><strong>Numer Zlecenia:</strong> {order.Number}</p>
                    <p><strong>Klient:</strong> {contractor.Name}</p>
                    <p><strong>Adres:</strong> {contractor.Street}, {contractor.ZipCode} {contractor.City}</p>
                    <p><strong>NIP:</strong> {contractor.NIP}</p>
                    <p><strong>Email:</strong> {contractor.Email}</p>
                </div>
                <table>
                    <thead>
                        <tr>
                            <th>Nazwa</th>
                            <th>Ilość</th>
                            <th>Cena Jednostkowa (PLN)</th>
                            <th>Wartość (PLN)</th>
                            <th>Stawka VAT</th>
                        </tr>
                    </thead>
                    <tbody>";

        // Loop through items to create table rows
        foreach (var item in items)
        {
            htmlContent += $@"
            <tr>
                <td>{item.NazwaPelna}</td>
                <td>{item.Ilosc}</td>
                <td>{item.CenaJednostkowa:F2}</td>
                <td>{item.CenaJednostkowa * item.Ilosc:F2}</td>
                <td>{item.StawkaVat}%</td>
            </tr>";
        }

        htmlContent += @"
                    </tbody>
                </table>
            </body>
        </html>";

        using var memoryStream = new MemoryStream();

        // Convert HTML to PDF and write to MemoryStream
        HtmlConverter.ConvertToPdf(htmlContent, memoryStream);

        return memoryStream.ToArray(); // Return byte array
    }
}