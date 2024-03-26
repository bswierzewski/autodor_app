using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Contractors.Queries.GetContractors;
public class GetContractorsQuery : IRequest<IEnumerable<Contractor>> { }

public class GetContractorsQueryHandler : IRequestHandler<GetContractorsQuery, IEnumerable<Contractor>>
{
    private readonly IContractorService _contractorService;

    public GetContractorsQueryHandler(IContractorService contractorService)
    {
        _contractorService = contractorService;
    }

    public async Task<IEnumerable<Contractor>> Handle(GetContractorsQuery request, CancellationToken cancellationToken)
    {
        return await _contractorService.GetAsync();
    }
}