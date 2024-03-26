using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.IFirmaSettings.Commands;

public record UpdateIFirmaSettingCommand() : IRequest<bool>
{
    public int Id { get; set; }
    public string User { get; set; }
    public string FakturaKey { get; set; }
}

public class UpdateIFirmaSettingCommandHandler : IRequestHandler<UpdateIFirmaSettingCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateIFirmaSettingCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateIFirmaSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.IFirmaSettings
            .FindAsync(new object[] { request.Id }, cancellationToken);

        _mapper.Map(request, entity);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
