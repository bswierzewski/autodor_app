namespace Domain.Entities.Settings;

public class IFirma : BaseAuditableEntity
{
    public string User { get; set; }
    public string FakturaApiKey { get; set; }
}
