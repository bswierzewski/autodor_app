using Domain.Entities;

namespace Application.Orders.Queries
{
    public class OrderDto
    {
        public bool IsSelected { get; set; } = true;
        public DateTime Date { get; set; }
        public string? Id { get; set; }
        public string? Number { get; set; }
        public string? Person { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }

        public int ItemsCount => Items?.Count() ?? 0;
        public decimal TotalPrice => Math.Round((Items?.Sum(x => x.TotalPrice) ?? 0) * 1.23M, 2);
    }
}
