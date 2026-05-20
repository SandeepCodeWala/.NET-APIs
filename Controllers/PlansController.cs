using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;

namespace MobileAppAPI.Controllers;

[ApiController]
[Route("api/plans")]
public class PlansController : ControllerBase
{
    private static List<Plan> plans = new List<Plan>
    {
        new Plan
        {
            Id="1",
            Name="Business Essential",
            Price="30.00",
            DataAllowance="25",
            Duration="month",
            Category="voicedata",
            PlanCode="VEM0001",
            SimType="Physical",
            Color="bg-white",
            PriceColor="text-gray-900",
            Features=new List<string>
            {
                "5G network with 150Mbps maximum speed",
                "Speeds capped at 1.5Mbps after 25GB",
                "Unlimited national talk & text"
            }
        },

        new Plan
        {
            Id="2",
            Name="Business Everyday",
            Price="40.00",
            DataAllowance="50",
            Duration="month",
            Category="voicedata",
            PlanCode="VEM0002",
            SimType="eSIM",
            Color="bg-violet-600",
            PriceColor="text-white",
            Features=new List<string>
            {
                "5G network with 150Mbps maximum speed",
                "Unlimited national talk & text"
            }
        },

        new Plan
        {
            Id="5",
            Name="Data Essential",
            Price="20.00",
            DataAllowance="30",
            Duration="month",
            Category="dataonly",
            PlanCode="VEM0004",
            SimType="Physical",
            Color="bg-blue-50",
            PriceColor="text-blue-900",
            Features=new List<string>
            {
                "Data only - no voice or text",
                "Perfect for tablets"
            }
        },

        new Plan
        {
            Id="8",
            Name="4G Backup Basic",
            Price="15.00",
            DataAllowance="10",
            Duration="month",
            Category="4gbackup",
            PlanCode="VEM0007",
            SimType="Physical",
            Color="bg-green-50",
            PriceColor="text-green-900",
            Features=new List<string>
            {
                "4G backup connectivity",
                "Emergency backup only"
            }
        }
    };

    private static List<Cart> carts = new List<Cart>();


[HttpGet]
public IActionResult GetPlans(
    [FromHeader(Name = "category")] string? category,
    [FromHeader(Name = "simType")] string? simType)
{
    var result = plans.AsQueryable();

    // Filter only if category is provided
    if (!string.IsNullOrEmpty(category))
    {
        result = result.Where(p =>
            p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
    }

    // Filter only if simType is provided
    if (!string.IsNullOrEmpty(simType))
    {
        result = result.Where(p =>
            p.SimType.Equals(simType, StringComparison.OrdinalIgnoreCase));
    }

    return Ok(new ApiResponse
    {
        status = 1,
        message = "Plans fetched successfully",
        data = result.ToList()
    });
}


[HttpPost("cart/add")]
public IActionResult AddToCart([FromBody] AddToCartRequest request)
{
    try
    {
        if (request == null || string.IsNullOrWhiteSpace(request.PlanId) || request.Quantity <= 0)
        {
            return BadRequest(new ApiResponse
            {
                status = 0,
                message = "Invalid cart request"
            });
        }

        // Find plan
        var plan = plans.FirstOrDefault(p => p.Id == request.PlanId.Trim());

        if (plan == null)
        {
            return NotFound(new ApiResponse
            {
                status = 0,
                message = "Plan not found"
            });
        }

        // Create new cart (1 checkout = 1 cart)
        var checkoutId = Guid.NewGuid().ToString();

        var cartItem = new CartItem
        {
            PlanId = plan.Id,
            PlanName = plan.Name,
            Price = plan.Price,
            Quantity = request.Quantity,
            SimType = plan.SimType
        };

        var total = Convert.ToDecimal(plan.Price) * request.Quantity;

        var cart = new Cart
        {
            CheckoutId = checkoutId,
            Items = new List<CartItem> { cartItem },
            TotalAmount = total
        };

        carts.Add(cart);

        return Ok(new ApiResponse
        {
            status = 1,
            message = "Item added to cart",
            data = new
            {
                checkoutId = checkoutId,
                totalAmount = total
            }
        });
    }
    catch (Exception ex)
    {
        return BadRequest(new ApiResponse
        {
            status = 0,
            message = ex.Message
        });
    }
}

[HttpGet("cart")]
public IActionResult GetCart()
{
    var checkoutId = Request.Headers["checkout_id"].FirstOrDefault();

    if (string.IsNullOrWhiteSpace(checkoutId))
    {
        return BadRequest(new ApiResponse
        {
            status = 0,
            message = "checkout_id header is required"
        });
    }

    var cart = carts.FirstOrDefault(c => c.CheckoutId == checkoutId);

    if (cart == null)
    {
        return NotFound(new ApiResponse
        {
            status = 0,
            message = "Cart not found"
        });
    }

    return Ok(new ApiResponse
    {
        status = 1,
        message = "Cart fetched successfully",
        data = cart
    });
}

}