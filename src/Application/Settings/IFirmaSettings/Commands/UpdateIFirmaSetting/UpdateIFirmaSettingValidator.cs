namespace Application.Settings.IFirmaSettings.Commands;

public class UpdateIFirmaSettingValidator : AbstractValidator<UpdateIFirmaSettingCommand>
{
    public UpdateIFirmaSettingValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.User)
            .NotEmpty();

        RuleFor(x => x.FakturaKey)
            .NotEmpty();
    }
}
