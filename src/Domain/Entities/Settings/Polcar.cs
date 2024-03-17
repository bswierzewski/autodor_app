namespace Domain.Entities.Settings;

public class Polcar : BaseAuditableEntity
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string DistributorCode { get; set; }
    public int BranchId { get; set; }
    public int LanguageId { get; set; }
}
