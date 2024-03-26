using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Contractors.Commands.DeleteContractor;
public class DeleteContractorCommand : IRequest
{
    public string Id { get; set; }
}

public class DeleteContractorCommandHandler : IRequestHandler<DeleteContractorCommand>
{
    private readonly ILogger<DeleteContractorCommandHandler> _logger;
    private readonly IContractorService _contractorService;

    public DeleteContractorCommandHandler(ILogger<DeleteContractorCommandHandler> logger,
        IContractorService contractorService)
    {
        _logger = logger;
        _contractorService = contractorService;
    }

    public async Task Handle(DeleteContractorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _contractorService.DeleteAsync(request.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            throw;
        }
    }
}