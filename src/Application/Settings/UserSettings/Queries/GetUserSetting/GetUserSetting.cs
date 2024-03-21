using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.UserSettings.Queries;

public record GetUserSettingQuery(int Id) : IRequest<UserSettingDto>;

public class GetUserSettingQueryHandler : IRequestHandler<GetUserSettingQuery, UserSettingDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserSettingQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserSettingDto> Handle(GetUserSettingQuery request, CancellationToken cancellationToken)
    {
        var UserSetting = await _context
            .UserSettings
            .Include(x => x.PolcarSetting)
            .Include(x => x.MongoDBSetting)
            .Include(x => x.IFirmaSetting)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (UserSetting == null)
            throw new NotFoundException();

        return _mapper.Map<UserSettingDto>(UserSetting);
    }
}
