using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.MongoDBSettings.Commands;

public record UpdateMongoDBSettingCommand() : IRequest<bool>
{
    public int Id { get; set; }
    public string CollectionName { get; set; }
}

public class UpdateMongoDBSettingCommandHandler : IRequestHandler<UpdateMongoDBSettingCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateMongoDBSettingCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateMongoDBSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MongoDBSettings
            .FindAsync(new object[] { request.Id }, cancellationToken);

        _mapper.Map(request, entity);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
