namespace Domain.Entities.Settings;

public class MongoDB : BaseAuditableEntity
{
    public string ConnectionURI { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}
