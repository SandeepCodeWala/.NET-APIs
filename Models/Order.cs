namespace MobileAppAPI.Models;

public class Order
{
    public string OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal Amount { get; set; }

    public string BillingCycle { get; set; }

    public string Status { get; set; }

    public List<OrderPlan> Plans { get; set; }

    public List<OrderTracking> Tracking { get; set; }
}

public class OrderPlan
{
    public string PlanName { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string Data { get; set; }

    public int Quantity { get; set; }

    public string SimType { get; set; }

    public decimal MonthlyCost { get; set; }

    public string DeliveryStatus { get; set; }
}

public class OrderTracking
{
    public string Status { get; set; }

    public string Location { get; set; }

    public string Date { get; set; }

    public string Time { get; set; }

    public bool Completed { get; set; }
}