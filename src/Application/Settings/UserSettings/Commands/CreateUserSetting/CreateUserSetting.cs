using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Settings.UserSettings.Commands;

public record CreateUserSettingCommand() : IRequest<int>
{
    public string Auth0Id { get; set; }
    public int IFirmaSettingId { get; set; }
    public int PolcarSettingId { get; set; }
    public int MongoDBSettingId { get; set; }
}

public class CreateUserSettingCommandHandler : IRequestHandler<CreateUserSettingCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateUserSettingCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateUserSettingCommand request, CancellationToken cancellationToken)
    {
        var userSetting = _mapper.Map<UserSetting>(request);

        _context.UserSettings.Add(userSetting);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return userSetting.Id;
    }
}