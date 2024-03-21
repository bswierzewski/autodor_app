namespace Domain.Entities.Settings;

public class IFirmaSetting : BaseAuditableEntity
{
    public string Email { get; set; }
    public string FakturaApiKey { get; set; }
}
