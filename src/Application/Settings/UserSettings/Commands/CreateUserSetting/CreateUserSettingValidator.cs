namespace Application.Settings.UserSettings.Commands;

public class CreateUserSettingValidator : AbstractValidator<CreateUserSettingCommand>
{
    public CreateUserSettingValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.IFirmaSettingId)
            .NotEmpty();
        
        RuleFor(x => x.MongoDBSettingId)
            .NotEmpty();
        
        RuleFor(x => x.PolcarSettingId)
            .NotEmpty();
    }
}
