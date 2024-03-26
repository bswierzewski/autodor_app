namespace Application.Settings.MongoDBSettings.Commands;

public class UpdateMongoDBSettingValidator : AbstractValidator<UpdateMongoDBSettingCommand>
{
    public UpdateMongoDBSettingValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.CollectionName)
            .NotEmpty();
    }
}
