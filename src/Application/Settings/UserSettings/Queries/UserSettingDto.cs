namespace Application.Settings.UserSettings.Queries;

public class UserSettingDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FakturaEmail { get; set; }
    public string DistributorCode { get; set; }
    public int BranchId { get; set; }
    public int LanguageId { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}
