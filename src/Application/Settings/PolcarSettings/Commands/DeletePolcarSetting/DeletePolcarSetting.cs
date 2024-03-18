using Application.Common.Interfaces;

namespace Application.Settings.PolcarSettings.Commands;

public record DeletePolcarSettingCommand(int Id) : IRequest<int>;

public class DeletePolcarSettingCommandHandler : IRequestHandler<DeletePolcarSettingCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public DeletePolcarSettingCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<int> Handle(DeletePolcarSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PolcarSettings.FindAsync(request.Id, cancellationToken);

        if (entity == null)
            throw new Exception("Polcar settings does't exist");

        _context.PolcarSettings.Remove(entity);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

