using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.MongoDBSettings.Queries
{
    public record GetMongoDBSettingsQuery : IRequest<List<MongoDBSettingDto>>
    {
    }

    public class GetMongoDBSettingsQueryHandler : IRequestHandler<GetMongoDBSettingsQuery, List<MongoDBSettingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMongoDBSettingsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MongoDBSettingDto>> Handle(GetMongoDBSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = await _context.MongoDBSettings.ToListAsync(cancellationToken: cancellationToken);

            return _mapper.Map<List<MongoDBSettingDto>>(settings);
        }
    }
}