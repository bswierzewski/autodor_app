using Domain.Entities;

namespace Application.Common.Interfaces;
public interface IContractorService
{
    public Task<IEnumerable<Contractor>> GetAsync();
    public Task CreateAsync(Contractor contractor);
    public Task DeleteAsync(string id);
}
