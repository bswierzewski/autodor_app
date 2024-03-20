namespace Application.Settings.PolcarSettings.Commands;

public class CreatePolcarSettingValidator : AbstractValidator<CreatePolcarSettingCommand>
{
    public CreatePolcarSettingValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.DistributorCode)
            .NotEmpty();

        RuleFor(x => x.BranchId)
            .NotEmpty();

        RuleFor(x => x.LanguageId)
            .NotEmpty();
    }
}
