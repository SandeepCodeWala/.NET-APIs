using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;

namespace MobileAppAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{

    private static List<Order> orders = new List<Order>
    {
        new Order
        {
            OrderId="ORD-000001171753",
            OrderDate=DateTime.Parse("2025-10-14"),
            Amount=260,
            BillingCycle="per month",
            Status="inprogress",
            Plans=new List<OrderPlan>(),
            Tracking=new List<OrderTracking>()
        },

        new Order
        {
            OrderId="ORD-000002072754",
            OrderDate=DateTime.Parse("2025-09-27"),
            Amount=330,
            BillingCycle="per month",
            Status="completed",

            Plans=new List<OrderPlan>
            {
                new OrderPlan
                {
                    PlanName="Business Everyday",
                    Description="Voice and Shared Data",
                    Price=40,
                    Data="50GB",
                    Quantity=2,
                    SimType="Physical SIM",
                    MonthlyCost=80,
                    DeliveryStatus="Delivered on September 27, 2025"
                },

                new OrderPlan
                {
                    PlanName="Business Best Value",
                    Description="Voice and Shared Data",
                    Price=50,
                    Data="120GB",
                    Quantity=5,
                    SimType="eSIM",
                    MonthlyCost=250,
                    DeliveryStatus="Delivered on September 27, 2025"
                }
            },

            Tracking=new List<OrderTracking>
            {
                new OrderTracking
                {
                    Status="Successful delivery",
                    Location="The Ponds, NSW",
                    Date="08-Oct-2025",
                    Time="1:20 PM",
                    Completed=true
                },

                new OrderTracking
                {
                    Status="On for delivery",
                    Location="Bungarribee, NSW",
                    Date="08-Oct-2025",
                    Time="7:48 AM",
                    Completed=false
                },

                new OrderTracking
                {
                    Status="Depot scan",
                    Location="Bungarribee, NSW",
                    Date="07-Oct-2025",
                    Time="11:30 AM",
                    Completed=false
                },

                new OrderTracking
                {
                    Status="Electronic billing transfer",
                    Location="Melbourne, VIC",
                    Date="06-Oct-2025",
                    Time="12:02 PM",
                    Completed=false
                }
            }
        }
    };



    // ORDER HISTORY LIST
    [HttpGet]
    public IActionResult GetOrders(string status)
    {
        try
        {
            var query = orders.AsQueryable();

             if (!string.IsNullOrEmpty(status) && status.ToLower() != "all")
        {
            query = query.Where(o => o.Status.ToLower() == status.ToLower());
        }
            var result = query
              
                .Select(o => new
                {
                    o.OrderId,
                    o.OrderDate,
                    o.Amount,
                    o.BillingCycle,
                    o.Status
                })
                .ToList();

            return Ok(new ApiResponse
            {
                status = 1,
                message = "Orders fetched successfully",
                data = result
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



    // ORDER DETAILS (PLANS)
[HttpGet("details")]
public IActionResult GetOrderDetails()
{
    var orderId = Request.Headers["orderId"].FirstOrDefault();

    if (string.IsNullOrEmpty(orderId))
    {
        return BadRequest(new ApiResponse
        {
            status = 0,
            message = "orderId header is required"
        });
    }

    var order = orders.FirstOrDefault(x => x.OrderId == orderId);

    if (order == null)
    {
        return NotFound(new ApiResponse
        {
            status = 0,
            message = "Order not found"
        });
    }

    return Ok(new ApiResponse
    {
        status = 1,
        message = "Order details fetched successfully",
        data = order
    });
}


  [HttpGet("tracking")]
public IActionResult GetTracking()
{
    var orderId = Request.Headers["orderId"].FirstOrDefault();

    if (string.IsNullOrEmpty(orderId))
    {
        return BadRequest(new ApiResponse
        {
            status = 0,
            message = "orderId header is required"
        });
    }

    var order = orders.FirstOrDefault(x => x.OrderId == orderId);

    if (order == null)
    {
        return NotFound(new ApiResponse
        {
            status = 0,
            message = "Order not found"
        });
    }

    return Ok(new ApiResponse
    {
        status = 1,
        message = "Tracking fetched successfully",
        data = order.Tracking
    });
}
}