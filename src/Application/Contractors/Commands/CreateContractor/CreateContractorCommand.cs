using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Contractors.Commands.CreateContractor;

public class CreateContractorCommand : IRequest
{
    public string Name { get; set; }
    public string City { get; set; }
    public string NIP { get; set; }
    public string ZipCode { get; set; }
    public string Street { get; set; }
    public string Email { get; set; }
}

public class CreateContractorCommandHandler : IRequestHandler<CreateContractorCommand>
{
    private readonly ILogger<CreateContractorCommandHandler> _logger;
    private readonly IContractorService _contractorService;

    public CreateContractorCommandHandler(ILogger<CreateContractorCommandHandler> logger, 
        IContractorService contractorService)
    {
        _logger = logger;
        _contractorService = contractorService;
    }

    public async Task Handle(CreateContractorCommand request, CancellationToken cancellationToken)
    {
        await _contractorService.CreateAsync(new Contractor
        {
            CreatedAt = DateTime.Now,
            Name = request.Name,
            City = request.City,
            NIP = request.NIP,
            ZipCode = request.ZipCode,
            Street = request.Street,
            Email = request.Email
        });
    }
}
