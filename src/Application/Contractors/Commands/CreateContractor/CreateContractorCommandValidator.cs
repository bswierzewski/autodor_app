namespace Application.Contractors.Commands.CreateContractor;

public class CreateContractorCommandValidator : AbstractValidator<CreateContractorCommand>
{
    public CreateContractorCommandValidator()
    {
        RuleFor(c => c)
            .Must(command => !string.IsNullOrEmpty(command.Name) &&
                             !string.IsNullOrEmpty(command.City) &&
                             !string.IsNullOrEmpty(command.NIP) &&
                             !string.IsNullOrEmpty(command.ZipCode) &&
                             !string.IsNullOrEmpty(command.Street))
            .WithMessage("Wprowadź wszystkie dane kontrahenta.");
    }
}
