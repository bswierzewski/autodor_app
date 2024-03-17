using Domain.Entities.Settings;

namespace Domain.Entities
{
    public class UserSetting : BaseAuditableEntity
    {
        public string Email { get; set; }

        public int IFirmaId { get; set; }
        public IFirma IFirma { get; set; }
        public int PolcarId { get; set; }
        public Polcar Polcar { get; set; }
        public int MongoDBId { get; set; }
        public MongoDB MongoDB { get; set; }
    }
}
