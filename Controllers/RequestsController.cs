using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;
namespace MobileAppAPI.Controllers;

[ApiController]
[Route("api/requests")]
public class RequestsController : ControllerBase
{

    private static List<Request> requests = new List<Request>
    {
        new Request
        {
            RequestId="REQ-000002072754",
            Title="Suspend",
            RequestDate=DateTime.Parse("2025-10-14 12:33"),
            Status="in-progress",
            Items=new List<RequestItem>()
        },

        new Request
        {
            RequestId="REQ-000002072751",
            Title="Change number",
            RequestDate=DateTime.Parse("2025-10-01 12:33"),
            Status="completed",
            Items=new List<RequestItem>()
        },

        new Request
        {
            RequestId="REQ-000002072753",
            Title="Cancel",
            RequestDate=DateTime.Parse("2025-09-27 12:33"),
            Status="completed-with-failure",

            Items=new List<RequestItem>
            {
                new RequestItem
                {
                    MobileNumber="0490 055 582",
                    Status="failed",
                    Message="Service activation failed"
                },

                new RequestItem
                {
                    MobileNumber="0490 111 272",
                    Status="success",
                    Message=""
                },

                new RequestItem
                {
                    MobileNumber="0490 345 115",
                    Status="success",
                    Message=""
                }
            }
        },

        new Request
        {
            RequestId="REQ-000002072752",
            Title="Swap SIM",
            RequestDate=DateTime.Parse("2025-08-07 12:33"),
            Status="completed",
            Items=new List<RequestItem>()
        }
    };



    // REQUEST LIST API
    [HttpGet]
    public IActionResult GetRequests(string status = "all")
    {
        try
        {
            var query = requests.AsQueryable();

            // Apply filter only if status is not "all"
            if (!string.IsNullOrWhiteSpace(status) && status.ToLower() != "all")
            {
                query = query.Where(x => x.Status.ToLower() == status.ToLower());
            }

            var result = query.Select(x => new
            {
                x.RequestId,
                x.Title,
                x.RequestDate,
                x.Status
            }).ToList();

            return Ok(new ApiResponse
            {
                status = 1,
                message = "Requests fetched successfully",
                data = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse
            {
                status = 0,
                message = ex.Message,
                data = null
            });
        }
    }


    [HttpGet("details")]
    public IActionResult GetRequestDetails()
    {
        try
        {
            // Read from header
            var requestId = Request.Headers["X-Request-Id"].FirstOrDefault();

            if (!requestId.StartsWith("REQ-"))
            {
                return BadRequest(new ApiResponse
                {
                    status = 0,
                    message = "Invalid Request ID format"
                });
            }

            // ✅ Validate header
            if (string.IsNullOrWhiteSpace(requestId))
            {
                return BadRequest(new ApiResponse
                {
                    status = 0,
                    message = "X-Request-Id header is required"
                });
            }

            // Find request
            var request = requests.FirstOrDefault(x => x.RequestId == requestId);

            if (request == null)
            {
                return NotFound(new ApiResponse
                {
                    status = 0,
                    message = "Request not found"
                });
            }

            return Ok(new ApiResponse
            {
                status = 1,
                message = "Request details fetched successfully",
                data = request
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse
            {
                status = 0,
                message = "Something went wrong",
                data = ex.Message
            });
        }
    }
}