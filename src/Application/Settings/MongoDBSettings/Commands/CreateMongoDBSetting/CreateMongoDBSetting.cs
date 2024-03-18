using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.MongoDBSettings.Commands;

public record CreateMongoDBSettingCommand() : IRequest<int>
{

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
        return await Task.FromResult(0);
    }
}
