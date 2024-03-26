using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Settings.PolcarSettings.Commands;

public record UpdatePolcarSettingCommand() : IRequest<bool>
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string DistributorCode { get; set; }
    public int BranchId { get; set; }
    public int LanguageId { get; set; }
}

public class UpdatePolcarSettingCommandHandler : IRequestHandler<UpdatePolcarSettingCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdatePolcarSettingCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdatePolcarSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PolcarSettings
            .FindAsync(new object[] { request.Id }, cancellationToken);

        _mapper.Map(request, entity);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
