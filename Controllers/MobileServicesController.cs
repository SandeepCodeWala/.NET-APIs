using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;

namespace MobileAppAPI.Controllers;

[ApiController]
[Route("api/services")]
public class ServicesController : ControllerBase
{
    // ✅ SIM LIST
    private static List<MobileService> services = new List<MobileService>
    {
        new MobileService { MobileNumber = "0490 055 582", Plan = "Business Power $65 180GB" },
        new MobileService { MobileNumber = "0490 111 272", Plan = "Business Best Value $55 120GB" },
        new MobileService { MobileNumber = "0490 345 115", Plan = "Business Everyday $40 50GB" }
    };

    // ✅ ACTION MASTER LIST
    private static List<ServiceAction> allActions = new List<ServiceAction>
    {
        new ServiceAction { Id="1", Name="Activate", Icon="check-circle", Color="green" },
        new ServiceAction { Id="2", Name="Suspend", Icon="ban", Color="orange" },
        new ServiceAction { Id="3", Name="Resume", Icon="rotate-right", Color="blue" },
        new ServiceAction { Id="4", Name="Cancel", Icon="circle-xmark", Color="red" },
        new ServiceAction { Id="5", Name="Reactivate", Icon="check-double", Color="blue" },
        new ServiceAction { Id="6", Name="Swap SIM", Image="arrow_cross", Color="yellow" },
        new ServiceAction { Id="7", Name="Port In", Image="arrow_right_arc", Color="green" },
        new ServiceAction { Id="8", Name="Port Out", Image="arrow_right_from_arc", Color="red" },
        new ServiceAction { Id="9", Name="Change Number", Image="change_number", Color="purple" },
        new ServiceAction { Id="10", Name="Change Network Profile", Image="change_network", Color="purple" },
        new ServiceAction { Id="11", Name="Resend eSIM QR Code", Icon="📱", Color="yellow" }
    };

    // ACTION MAPPING
    private static Dictionary<string, List<string>> simActionsMap = new Dictionary<string, List<string>>
    {
        { "0490 055 582", new List<string> { "2", "4" } }, // Suspend, Cancel
        { "0490 111 272", new List<string> { "1", "3", "6" } }, // Activate, Resume, Swap
        { "0490 345 115", new List<string> { "2", "9", "10" } } // Suspend, Change Number, Network
    };

    // ============================
    // 1️⃣ GET MOBILE SERVICES
    // ============================
    [HttpGet("list")]
    public IActionResult GetServices()
    {
        return Ok(new ApiResponse
        {
            status = 1,
            message = "Mobile services fetched successfully",
            data = services
        });
    }

    // ============================
    // 2️⃣ GET ACTIONS BASED ON SIM
    // ============================
    [HttpGet("actions")]
    public IActionResult GetActions()
    {
        var mobileNumber = Request.Headers["mobile_number"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(mobileNumber))
        {
            return BadRequest(new ApiResponse
            {
                status = 0,
                message = "mobile_number header is required"
            });
        }

        if (!simActionsMap.ContainsKey(mobileNumber))
        {
            return NotFound(new ApiResponse
            {
                status = 0,
                message = "No actions found for this number"
            });
        }

        var actionIds = simActionsMap[mobileNumber];

        var result = allActions.Where(a => actionIds.Contains(a.Id)).ToList();

        return Ok(new ApiResponse
        {
            status = 1,
            message = "Actions fetched successfully",
            data = result
        });
    }

    // ============================
    // 3️⃣ POST SUBMIT ACTION
    // ============================
    [HttpPost("submit")]
    public IActionResult SubmitAction([FromBody] SubmitServiceActionRequest request)
    {
        try
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse
                {
                    status = 0,
                    message = "Invalid request"
                });
            }

            if (string.IsNullOrWhiteSpace(request.MobileNumber) ||
                string.IsNullOrWhiteSpace(request.ActionId) ||
                request.ScheduledDate == default ||
                string.IsNullOrWhiteSpace(request.Reason))
            {
                return BadRequest(new ApiResponse
                {
                    status = 0,
                    message = "All fields are required"
                });
            }

            return Ok(new ApiResponse
            {
                status = 1,
                message = "Service request submitted successfully",
                data = request
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
}