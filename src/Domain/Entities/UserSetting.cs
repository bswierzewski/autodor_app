using Domain.Entities.Settings;

namespace Domain.Entities
{
    public class UserSetting : BaseAuditableEntity
    {
        public string Auth0Id { get; set; }

        public int? IFirmaSettingId { get; set; }
        public IFirmaSetting IFirmaSetting { get; set; }
        public int? PolcarSettingId { get; set; }
        public PolcarSetting PolcarSetting { get; set; }
        public int? MongoDBSettingId { get; set; }
        public MongoDBSetting MongoDBSetting { get; set; }
    }
}
