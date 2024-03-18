using Application.Common.Interfaces;

namespace Application.Settings.MongoDBSettings.Commands;

public record DeleteMongoDBSettingCommand(int Id) : IRequest<int>;

public class DeleteMongoDBSettingCommandHandler : IRequestHandler<DeleteMongoDBSettingCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public DeleteMongoDBSettingCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<int> Handle(DeleteMongoDBSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MongoDBSettings.FindAsync(request.Id, cancellationToken);

        if (entity == null)
            throw new Exception("MongoDB settings does't exist");

        _context.MongoDBSettings.Remove(entity);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

