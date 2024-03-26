namespace Application.Settings.IFirmaSettings.Commands;

public class CreateIFirmaSettingValidator : AbstractValidator<CreateIFirmaSettingCommand>
{
    public CreateIFirmaSettingValidator()
    {
        RuleFor(x => x.User)
            .NotEmpty();

        RuleFor(x => x.FakturaKey)
            .NotEmpty();
    }
}
