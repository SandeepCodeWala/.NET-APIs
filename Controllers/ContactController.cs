using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;

namespace MobileAppAPI.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactsController : ControllerBase
{
    // Temporary in-memory storage
    private static List<Contact> contacts = new List<Contact>();
    private static int nextId = 1;

    // GET ALL CONTACTS
    [HttpGet]
    public IActionResult GetContacts()
    {
        return Ok(new ApiResponse
        {
            status = 1,
            message = "Contacts fetched successfully",
            data = contacts
        });
    }

    // ADD CONTACT
    [HttpPost]
    public IActionResult AddContact([FromBody] Contact contact)
    {
        contact.Id = nextId++;
        contacts.Add(contact);

        return Ok(new ApiResponse
        {
            status = 1,
            message = "Contact Added successfully",
            data = contacts
        });
    }

    // UPDATE CONTACT
    [HttpPut("{id}")]
    public IActionResult UpdateContact(int id, [FromBody] Contact updatedContact)
    {
        var existingContact = contacts.FirstOrDefault(c => c.Id == id);

        if (existingContact == null)
        {
            return NotFound(new ApiResponse
            {
                status = 0,
                message = "Contact Not Found",
                data = null
            });
        }

        existingContact.ContactName = updatedContact.ContactName;
        existingContact.Email = updatedContact.Email;
        existingContact.ContactNumber = updatedContact.ContactNumber;

        return Ok(new ApiResponse
        {
            status = 1,
            message = "Contact updated successfully",
            data = existingContact
        });
    }
}