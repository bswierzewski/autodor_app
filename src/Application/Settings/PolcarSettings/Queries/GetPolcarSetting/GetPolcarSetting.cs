using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.PolcarSettings.Queries;

public record GetPolcarSettingQuery(int Id) : IRequest<PolcarSettingDto>;

public class GetPolcarSettingQueryHandler : IRequestHandler<GetPolcarSettingQuery, PolcarSettingDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPolcarSettingQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PolcarSettingDto> Handle(GetPolcarSettingQuery request, CancellationToken cancellationToken)
    {
        var polcarSetting = await _context
            .PolcarSettings
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (polcarSetting == null)
            throw new NotFoundException();

        return _mapper.Map<PolcarSettingDto>(polcarSetting);
    }
}
