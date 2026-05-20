using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;

namespace MobileAppAPI.Controllers;

[ApiController]
[Route("api/deliveryAddress")]
public class DeliveryController : ControllerBase
{
    // Temporary in-memory storage
    private static List<Delivery> deliveryAddresses = new List<Delivery>();
    private static int nextId = 1;

    // GET ALL CONTACTS
    [HttpGet]
    public IActionResult GetDeliveryAddresses()
    {
        return Ok(new ApiResponse
        {
            status = 1,
            message = "Delivery addresses fetched successfully",
            data = deliveryAddresses
        });
    }

    // ADD CONTACT
    [HttpPost]
    public IActionResult AddDeliveryAddress([FromBody] Delivery address)
    {
        address.Id = nextId++;
        deliveryAddresses.Add(address);

        return Ok(new ApiResponse
        {
            status = 1,
            message = "Delivery address added successfully",
            data = deliveryAddresses
        });
    }

    // UPDATE CONTACT
    [HttpPut("{id}")]
    public IActionResult UpdateDeliveryAddress(int id, [FromBody] Delivery updatedAddress)
    {
        var existingDelivery = deliveryAddresses.FirstOrDefault(c => c.Id == id);

        if (existingDelivery == null)
        {
            return NotFound(new ApiResponse
            {
                status = 0,
                message = "Delivery address not found",
                data = null
            });
        }

        existingDelivery.AddressLine1 = updatedAddress.AddressLine1;
        existingDelivery.Suburb = updatedAddress.Suburb;
        existingDelivery.State = updatedAddress.State;
        existingDelivery.PostCode = updatedAddress.PostCode;

        return Ok(new ApiResponse
        {
            status = 1,
            message = "Delivery address updated successfully",
            data = existingDelivery
        });
    }
}