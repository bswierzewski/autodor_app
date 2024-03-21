namespace Application.Settings.UserSettings.Commands;

public class CreateUserSettingValidator : AbstractValidator<CreateUserSettingCommand>
{
    public CreateUserSettingValidator()
    {
        RuleFor(x => x.Auth0Id)
            .NotEmpty();

        RuleFor(x => x.IFirmaSettingId)
            .NotEmpty();
        
        RuleFor(x => x.MongoDBSettingId)
            .NotEmpty();
        
        RuleFor(x => x.PolcarSettingId)
            .NotEmpty();
    }
}
