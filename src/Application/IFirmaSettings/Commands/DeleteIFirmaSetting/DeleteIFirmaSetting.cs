using Application.Common.Interfaces;

namespace Application.Snippets.Commands.DeleteSnippet;

public record DeleteIFirmaSettingCommand(int Id) : IRequest<int>;

public class DeleteIFirmaSettingCommandHandler : IRequestHandler<DeleteIFirmaSettingCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public DeleteIFirmaSettingCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<int> Handle(DeleteIFirmaSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.IFirmaSettings.FindAsync(request.Id, cancellationToken);

        if (entity == null)
            throw new Exception("IFirmaSettings does't exist");

        _context.IFirmaSettings.Remove(entity);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

