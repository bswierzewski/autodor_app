using Application.Common.Interfaces;

namespace Application.Settings.UserSettings.Commands;

public record DeleteUserSettingCommand(int Id) : IRequest<int>;

public class DeleteUserSettingCommandHandler : IRequestHandler<DeleteUserSettingCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public DeleteUserSettingCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<int> Handle(DeleteUserSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.UserSettings.FindAsync(request.Id, cancellationToken);

        if (entity == null)
            throw new Exception("User settings does't exist");

        _context.UserSettings.Remove(entity);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

