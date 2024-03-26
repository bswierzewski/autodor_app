namespace Domain.Entities;

public class Order
{
    public bool IsSelected { get; set; } = true;
    public DateTime Date { get; set; }
    public string? Id { get; set; }
    public string? Number { get; set; }
    public string? Person { get; set; }
    public IEnumerable<OrderItem> Items { get; set; } = Enumerable.Empty<OrderItem>();
}
