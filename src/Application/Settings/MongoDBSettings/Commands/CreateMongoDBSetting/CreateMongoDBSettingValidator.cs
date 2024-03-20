namespace Application.Settings.MongoDBSettings.Commands;

public class CreateMongoDBSettingValidator : AbstractValidator<CreateMongoDBSettingCommand>
{
    public CreateMongoDBSettingValidator()
    {
        RuleFor(x => x.ConnectionURI)
            .NotEmpty();

        RuleFor(x => x.DatabaseName)
            .NotEmpty();

        RuleFor(x => x.CollectionName)
            .NotEmpty();
    }
}
