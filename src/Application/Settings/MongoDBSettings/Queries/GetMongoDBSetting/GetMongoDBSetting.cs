using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.MongoDBSettings.Queries
{
    public record GetMongoDBSettingQuery(int Id) : IRequest<MongoDBSettingDto>;

    public class GetMongoDBQueryHandler : IRequestHandler<GetMongoDBSettingQuery, MongoDBSettingDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMongoDBQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MongoDBSettingDto> Handle(GetMongoDBSettingQuery request, CancellationToken cancellationToken)
        {
            var mongoDBSetting = await _context
                .MongoDBSettings
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (mongoDBSetting == null)
                throw new NotFoundException();

            return _mapper.Map<MongoDBSettingDto>(mongoDBSetting);
        }
    }

}
