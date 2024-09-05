using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ExcludedOrder> ExcludedOrders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
