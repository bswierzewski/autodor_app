using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.IFirmaSettings.Queries
{
    public record GetIFirmaSettingsQuery : IRequest<List<IFirmaSettingDto>>
    {
    }

    public class GetIFirmaSettingsQueryHandler : IRequestHandler<GetIFirmaSettingsQuery, List<IFirmaSettingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetIFirmaSettingsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<IFirmaSettingDto>> Handle(GetIFirmaSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = await _context.IFirmaSettings.ToListAsync(cancellationToken: cancellationToken);

            return _mapper.Map<List<IFirmaSettingDto>>(settings);
        }
    }
}