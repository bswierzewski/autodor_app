namespace Application.Settings.PolcarSettings.Queries;

public class PolcarSettingDto
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string DistributorCode { get; set; }
    public int BranchId { get; set; }
    public int LanguageId { get; set; }
}
