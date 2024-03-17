namespace Domain.Entities.Settings;

public class IFirma : BaseAuditableEntity
{
    public string Email { get; set; }
    public string FakturaApiKey { get; set; }
}
