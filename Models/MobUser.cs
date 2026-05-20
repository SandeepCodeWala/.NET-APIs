namespace MobileAppAPI.Models;

public class MobUser
{
    public long MobUserId { get; set; }
    public string Uid { get; set; } = string.Empty;
    public string? OrgEmpId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string PrimMobNo { get; set; } = string.Empty;
    public string? SeconMobNo { get; set; }
    public string? Image { get; set; }
    public bool IsActive { get; set; }
    public bool IsLocked { get; set; }
    public DateTime CreatedDate { get; set; }
}
