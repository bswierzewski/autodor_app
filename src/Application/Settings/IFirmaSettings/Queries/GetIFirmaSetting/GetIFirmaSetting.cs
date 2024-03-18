using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.IFirmaSettings.Queries;

public record GetIFirmaSettingQuery(int Id) : IRequest<IFirmaSettingDto>;

public class GetIFirmaSettingQueryHandler : IRequestHandler<GetIFirmaSettingQuery, IFirmaSettingDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetIFirmaSettingQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IFirmaSettingDto> Handle(GetIFirmaSettingQuery request, CancellationToken cancellationToken)
    {
        var iFirmaSetting = await _context
            .IFirmaSettings
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (iFirmaSetting == null)
            throw new NotFoundException();

        return _mapper.Map<IFirmaSettingDto>(iFirmaSetting);
    }
}
