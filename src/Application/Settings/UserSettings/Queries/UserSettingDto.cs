namespace Application.Settings.UserSettings.Queries;

public class UserSettingDto
{
    public int Id { get; set; }
    public string Auth0Id { get; set; }
    public int IFirmaSettingId { get; set; }
    public string IFirmaEmail { get; set; }
    public int MongoDBSettingId { get; set; }
    public string MongoDBCollection { get; set; }
    public int PolcarSettingId { get; set; }
    public string PolcarDistributorCode { get; set; }
}
