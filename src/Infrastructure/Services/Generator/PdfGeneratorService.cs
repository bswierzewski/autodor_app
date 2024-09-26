using Application.Interfaces;
using iText.Html2pdf;

namespace Infrastructure.Services.Generator;

public class PDFGeneratorService() : IPDFGeneratorService
{
    public byte[] Generate(string htmlContent)
    {
        using var memoryStream = new MemoryStream();
        var converterProperties = GetConverterProperties();

        HtmlConverter.ConvertToPdf(htmlContent, memoryStream, converterProperties);

        return memoryStream.ToArray();
    }

    private ConverterProperties GetConverterProperties()
    {
        var converterProperties = new ConverterProperties();
        var fontProvider = new iText.Layout.Font.FontProvider("Arial");
        fontProvider.AddSystemFonts();
        converterProperties.SetFontProvider(fontProvider);

        return converterProperties;
    }
}
