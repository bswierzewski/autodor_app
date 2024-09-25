using Application.Interfaces;
using Application.Invoices.Commands.DTOs;
using Domain.Entities;
using iText.Html2pdf;
using System.Reflection;

namespace Infrastructure.Services.Printer;

public class PdfPrintService() : IPrintService
{
    public byte[] Print(Contractor contractor, Order order, List<Pozycje> items)
    {
        // Convert the embedded logo image to Base64
        string base64Logo = ConvertImageToBase64("Infrastructure.Resources.logo.png");

        // Start creating the HTML content
        string htmlContent = $@"
        <html>
            <head>
                <meta charset='utf-8' />
                <title>WZ (Wydanie Zewnętrzne)</title>
                <style>
                    body {{ font-family: sans-serif; }}
                    h1 {{ text-align: center; }}
                    table {{ width: 100%; border-collapse: collapse; margin-top: 20px; }}
                    th, td {{ border: 1px solid #000; padding: 8px; text-align: left; }}
                    th {{ background-color: #f2f2f2; }}
                    .header {{ margin-bottom: 20px; position: relative; }}
                    .logo {{ position: absolute; top: 0; right: 0; width: 150px; height: auto; }}
                </style>
            </head>
            <body>
                <div class='header'>
                    <img src='data:image/png;base64,{base64Logo}' alt='Company Logo' class='logo' />
                    <h1>Wydanie Zewnętrzne</h1>
                    <div><strong>Data:</strong> {order.Date:dd-MM-yyyy}</div>
                    <div><strong>Numer Zlecenia:</strong> {order.Number}</div>
                    <div><strong>Klient:</strong> {contractor.Name}</div>
                    <div><strong>Adres:</strong> {contractor.Street}, {contractor.ZipCode} {contractor.City}</div>
                    <div><strong>NIP:</strong> {contractor.NIP}</div>
                    <div><strong>Email:</strong> {contractor.Email}</div>
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

    // Helper method to convert embedded image to Base64
    private string ConvertImageToBase64(string resourcePath)
    {
        using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath) 
            ?? throw new FileNotFoundException($"Resource not found: {resourcePath}");

        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        byte[] imageBytes = memoryStream.ToArray();
        return Convert.ToBase64String(imageBytes);
    }

}