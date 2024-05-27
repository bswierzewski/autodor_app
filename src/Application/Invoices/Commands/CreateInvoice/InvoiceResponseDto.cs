using System.Text.Json.Serialization;

namespace Application.Invoice.Commands.CreateInvoice;

public class InvoiceResponseDto
{
    [JsonPropertyName("response")]
    public ResponseDto Response { get; set; }
    public override string ToString()
    {
        return $"{Response?.Kod} - {Response?.Informacja}";
    }
}

public class ResponseDto
{
    public int Kod { get; set; }
    public string Informacja { get; set; }
}