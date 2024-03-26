using Application.Common.Interfaces;

namespace Application.Settings.UserSettings.Commands;

public class CreateUserSettingValidator : AbstractValidator<CreateUserSettingCommand>
{
    public CreateUserSettingValidator(IApplicationDbContext dbContext)
    {
        RuleFor(x => x.Auth0Id)
            .NotEmpty();

        RuleFor(x => x.IFirmaSettingId)
            .NotEmpty()
            .Must(id => dbContext.IFirmaSettings.Any(setting => setting.Id == id))
            .WithMessage("Id doesn't exists");

        RuleFor(x => x.MongoDBSettingId)
            .NotEmpty()
            .Must(id => dbContext.MongoDBSettings.Any(setting => setting.Id == id))
            .WithMessage("Id doesn't exists");

        RuleFor(x => x.PolcarSettingId)
            .NotEmpty()
            .Must(id => dbContext.PolcarSettings.Any(setting => setting.Id == id))
            .WithMessage("Id doesn't exists");

    }
}
