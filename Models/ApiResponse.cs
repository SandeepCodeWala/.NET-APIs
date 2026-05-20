namespace MobileAppAPI.Models;

public class ApiResponse
{
    public int status { get; set; }
    public string message { get; set; }
    public object data { get; set; }
}