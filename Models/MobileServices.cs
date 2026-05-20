public class MobileService
{
    public string MobileNumber { get; set; }
    public string Plan { get; set; }
}

public class ServiceAction
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Color { get; set; }
    public string Image { get; set; }
}

public class SubmitServiceActionRequest
{
    public string MobileNumber { get; set; }
    public string ActionId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string Reason { get; set; }
}