namespace MobileAppAPI.Models;

public class Plan
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }
    public string DataAllowance { get; set; }
    public string Duration { get; set; }
    public List<string> Features { get; set; }
    public string Color { get; set; }
    public string PriceColor { get; set; }
    public string Category { get; set; }
    public string PlanCode { get; set; }
    public string SimType { get; set; }
}

public class AddToCartRequest
{
    public string PlanId { get; set; } = string.Empty;
    public int Quantity { get; set; }
}

public class CartItem
{
    public string PlanId { get; set; } = string.Empty;
    public string PlanName { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string SimType { get; set; } = string.Empty;
}

public class Cart
{
    public string CheckoutId { get; set; }
    public List<CartItem> Items { get; set; }
    public decimal TotalAmount { get; set; }
}