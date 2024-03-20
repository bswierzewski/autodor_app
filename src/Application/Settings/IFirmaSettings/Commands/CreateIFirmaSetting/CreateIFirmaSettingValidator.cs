namespace Application.Settings.IFirmaSettings.Commands;

public class CreateIFirmaSettingValidator : AbstractValidator<CreateIFirmaSettingCommand>
{
    public CreateIFirmaSettingValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.FakturaApiKey)
            .NotEmpty();
    }
}
