using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities.Settings;

namespace Application.Settings.IFirmaSettings.Commands;

public record CreateIFirmaSettingCommand() : IRequest<int>
{
    public string Email { get; set; }
    public string FakturaApiKey { get; set; }
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
        var setting = _mapper.Map<IFirma>(request);

        _context.IFirmaSettings.Add(setting);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return setting.Id;
    }
}
