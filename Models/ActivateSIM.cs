namespace MobileAppAPI.Models;

public class ActivateNewRequest
{
    public string SIMNumberId { get; set; } = string.Empty;
    public string NewSIMNumber { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}

public class ActivatePortRequest
{
    public string SIMNumberId { get; set; } = string.Empty;
    public string PortInNumber { get; set; } = string.Empty;
    public string ConnectionType { get; set; } = string.Empty;
    public string LosingCarrierAccountNumber { get; set; } = string.Empty;
}
