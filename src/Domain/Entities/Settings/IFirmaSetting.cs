namespace Domain.Entities.Settings;

public class IFirmaSetting : BaseAuditableEntity
{
    public string User { get; set; }
    public string FakturaKey { get; set; }
}
