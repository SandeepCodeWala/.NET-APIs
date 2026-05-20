namespace MobileAppAPI.Models;

public class Request
{
    public string RequestId { get; set; }

    public string Title { get; set; }

    public DateTime RequestDate { get; set; }

    public string Status { get; set; }

    public List<RequestItem> Items { get; set; }
}

public class RequestItem
{
    public string MobileNumber { get; set; }

    public string Status { get; set; }

    public string Message { get; set; }
}