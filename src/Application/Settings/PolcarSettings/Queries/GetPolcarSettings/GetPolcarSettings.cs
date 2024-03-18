using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.PolcarSettings.Queries;

public record GetPolcarSettingsQuery : IRequest<List<PolcarSettingDto>>
{
}

public class GetPolcarSettingsQueryHandler : IRequestHandler<GetPolcarSettingsQuery, List<PolcarSettingDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPolcarSettingsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PolcarSettingDto>> Handle(GetPolcarSettingsQuery request, CancellationToken cancellationToken)
    {
        var settings = await _context.PolcarSettings.ToListAsync(cancellationToken: cancellationToken);

        return _mapper.Map<List<PolcarSettingDto>>(settings);
    }
}