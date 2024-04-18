namespace Application.Settings.UserSettings.Commands;

public class UpdateUserSettingValidator : AbstractValidator<UpdateUserSettingCommand>
{
    public UpdateUserSettingValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.IFirmaSettingId)
            .NotEmpty();
        
        RuleFor(x => x.MongoDBSettingId)
            .NotEmpty();
        
        RuleFor(x => x.PolcarSettingId)
            .NotEmpty();
    }
}
