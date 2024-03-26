namespace Domain.Entities.Settings;

public class MongoDBSetting : BaseAuditableEntity
{
    public string CollectionName { get; set; }
}
