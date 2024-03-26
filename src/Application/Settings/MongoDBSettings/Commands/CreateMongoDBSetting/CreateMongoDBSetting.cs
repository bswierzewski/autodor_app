using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities.Settings;

namespace Application.Settings.MongoDBSettings.Commands;

public record CreateMongoDBSettingCommand() : IRequest<int>
{
    public string CollectionName { get; set; }
}

public class CreateMongoDBSettingCommandHandler : IRequestHandler<CreateMongoDBSettingCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateMongoDBSettingCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateMongoDBSettingCommand request, CancellationToken cancellationToken)
    {
        var setting = _mapper.Map<MongoDBSetting>(request);

        _context.MongoDBSettings.Add(setting);

        await _context.SaveChangesAsync(cancellationToken);

        return setting.Id;
    }
}
