using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities.Settings;

namespace Application.Settings.PolcarSettings.Commands;

public record CreatePolcarSettingCommand() : IRequest<int>
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string DistributorCode { get; set; }
    public int BranchId { get; set; }
    public int LanguageId { get; set; }
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
        var setting = _mapper.Map<Polcar>(request);

        _context.PolcarSettings.Add(setting);

        await _context.SaveChangesAsync(cancellationToken);

        return setting.Id;
    }
}
