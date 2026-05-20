using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;

namespace MobileAppAPI.Controllers;

[ApiController]
[Route("api/simdata")]
public class SimController : ControllerBase
{
    private static List<object> newNumberSimList = new List<object>
{
    new { Id = "SIM1", Number = "8545123456789012345", Plan="Business Power $65 180GB"},
    new { Id = "SIM2", Number = "8545123456789012356", Plan="Business Best Value $55 120GB"}
};

    private static List<object> portInSimList = new List<object>
{
    new { Id = "SIM3", Number = "9545123456789012378", Plan="Port Special $50 150GB"},
    new { Id = "SIM4", Number = "9545123456789012399", Plan="Port Premium $70 200GB"}
};


    [HttpGet("list")]
    public IActionResult GetSimList()
    {
        try
        {

            var type = Request.Headers["sim_type"].FirstOrDefault(); // new / port-in

            if (string.IsNullOrWhiteSpace(type))
            {
                return BadRequest(new ApiResponse
                {
                    status = 0,
                    message = "SIM-Type header is required (new / port-in)"
                });
            }
            object data;

            if (type == "new")
            {
                data = newNumberSimList;
            }
            else if (type == "port-in")
            {
                data = portInSimList;
            }
            else
            {
                return BadRequest(new ApiResponse
                {
                    status = 0,
                    message = "Invalid type. Use 'new' or 'port-in'"
                });
            }

            // same SIM list for both (as per your requirement)
            return Ok(new ApiResponse
            {
                status = 1,
                message = $"SIM list fetched for {type}",
                data = data
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


   [HttpPost("activate-new")]
public IActionResult ActivateNew([FromBody] ActivateNewRequest request)
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

        if (string.IsNullOrWhiteSpace(request.SIMNumberId) ||
            string.IsNullOrWhiteSpace(request.NewSIMNumber) ||
            string.IsNullOrWhiteSpace(request.Reason))
        {
            return BadRequest(new ApiResponse
            {
                status = 0,
                message = "All fields are required for NEW SIM"
            });
        }

        return Ok(new ApiResponse
        {
            status = 1,
            message = "New SIM activation request sent successfully",
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

   [HttpPost("activate-port")]
public IActionResult ActivatePort([FromBody] ActivatePortRequest request)
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

        if (string.IsNullOrWhiteSpace(request.SIMNumberId) ||
            string.IsNullOrWhiteSpace(request.PortInNumber) ||
            string.IsNullOrWhiteSpace(request.ConnectionType) ||
            string.IsNullOrWhiteSpace(request.LosingCarrierAccountNumber))
        {
            return BadRequest(new ApiResponse
            {
                status = 0,
                message = "All fields are required for PORT-IN"
            });
        }

        return Ok(new ApiResponse
        {
            status = 1,
            message = "Port-in SIM activation request sent successfully",
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