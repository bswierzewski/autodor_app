namespace Application.Settings.MongoDBSettings.Commands;

public class CreateMongoDBSettingValidator : AbstractValidator<CreateMongoDBSettingCommand>
{
    public CreateMongoDBSettingValidator()
    {
        RuleFor(x => x.CollectionName)
            .NotEmpty();
    }
}
