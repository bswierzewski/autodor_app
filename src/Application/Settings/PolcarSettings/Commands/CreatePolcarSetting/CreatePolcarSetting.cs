using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.PolcarSettings.Commands;

public record CreatePolcarSettingCommand() : IRequest<int>
{

}

public class CreatePolcarSettingCommandHandler : IRequestHandler<CreatePolcarSettingCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreatePolcarSettingCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreatePolcarSettingCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(0);
    }
}
