namespace Domain.Entities.Settings;

public class MongoDBSetting : BaseAuditableEntity
{
    public string ConnectionURI { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}
