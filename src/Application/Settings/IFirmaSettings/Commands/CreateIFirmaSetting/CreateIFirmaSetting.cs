using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.IFirmaSettings.Commands;

public record CreateIFirmaSettingCommand() : IRequest<int>
{

}

public class CreateIFirmaSettingCommandHandler : IRequestHandler<CreateIFirmaSettingCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateIFirmaSettingCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateIFirmaSettingCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(0);
    }
}
