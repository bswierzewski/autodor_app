using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.UserSettings.Commands;

public record UpdateUserSettingCommand() : IRequest<bool>
{
    public int Id { get; set; }
    public int IFirmaSettingId { get; set; }
    public int PolcarSettingId { get; set; }
    public int MongoDBSettingId { get; set; }
}

public class UpdateUserSettingCommandHandler : IRequestHandler<UpdateUserSettingCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateUserSettingCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateUserSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.UserSettings
            .FindAsync(new object[] { request.Id }, cancellationToken);

        _mapper.Map(request, entity);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}