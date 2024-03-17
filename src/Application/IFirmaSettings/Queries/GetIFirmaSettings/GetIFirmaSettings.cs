using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Snippets.Queries.GetSnippets
{
    public record GetIFirmaSettingsQuery : IRequest<List<IFirmaSettingDto>>
    {
    }

    public class GetSnippetsQueryHandler : IRequestHandler<GetIFirmaSettingsQuery, List<IFirmaSettingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSnippetsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<IFirmaSettingDto>> Handle(GetIFirmaSettingsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.IFirmaSettings.ToListAsync(cancellationToken: cancellationToken);

            return _mapper.Map<List<IFirmaSettingDto>>(result);
        }
    }
}