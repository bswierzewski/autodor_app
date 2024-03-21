using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.UserSettings.Queries;

public record GetUserSettingsQuery : IRequest<List<UserSettingDto>>
{
}

public class GetUserSettingsQueryHandler : IRequestHandler<GetUserSettingsQuery, List<UserSettingDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserSettingsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserSettingDto>> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
    {
        var settings = await _context
            .UserSettings
            .Include(x => x.PolcarSetting)
            .Include(x => x.MongoDBSetting)
            .Include(x => x.IFirmaSetting)
            .ToListAsync(cancellationToken: cancellationToken);

        return _mapper.Map<List<UserSettingDto>>(settings);
    }
}