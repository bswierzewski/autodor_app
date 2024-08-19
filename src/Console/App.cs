using Application.Orders.Queries;
using MediatR;

public class App(ISender sender)
{
    public async Task Run()
    {
        var orders = await sender.Send(new GetOrdersQuery
        {
            DateFrom = DateTime.Now.AddDays(-7),
            DateTo = DateTime.Now
        });

        Console.ReadLine();

        // Get all orders

        // Group by person

        // Set invoice per person
    }
}
