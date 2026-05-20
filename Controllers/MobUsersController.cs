using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;

namespace MobileAppAPI.Controllers;

[Route("api/mobUsers")]
[ApiController]
public class MobUsersController : ControllerBase
{
    private static List<MobUser> users = new List<MobUser>
    {
        new MobUser { MobUserId=1, Uid="U001", FirstName="John", LastName="Smith", Email="john.smith@example.com", PrimMobNo="0490055582", IsActive=true, IsLocked=false, CreatedDate=new DateTime(2024,1,10,0,0,0,DateTimeKind.Utc) },
        new MobUser { MobUserId=2, Uid="U002", FirstName="Sarah", LastName="Connor", Email="sarah.connor@example.com", PrimMobNo="0490111272", IsActive=true, IsLocked=false, CreatedDate=new DateTime(2024,2,15,0,0,0,DateTimeKind.Utc) },
        new MobUser { MobUserId=3, Uid="U003", FirstName="Mike", LastName="Johnson", Email="mike.johnson@example.com", PrimMobNo="0490345115", IsActive=false, IsLocked=true, CreatedDate=new DateTime(2024,3,20,0,0,0,DateTimeKind.Utc) }
    };

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(new ApiResponse { status = 1, message = "Users fetched successfully", data = users });
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(long id)
    {
        var user = users.FirstOrDefault(u => u.MobUserId == id);
        if (user == null)
            return NotFound(new ApiResponse { status = 0, message = "User not found" });

        return Ok(new ApiResponse { status = 1, message = "User fetched successfully", data = user });
    }

    [HttpPost]
    public IActionResult AddUser([FromBody] MobUser user)
    {
        user.MobUserId = users.Count > 0 ? users.Max(u => u.MobUserId) + 1 : 1;
        user.CreatedDate = DateTime.UtcNow;
        users.Add(user);
        return Ok(new ApiResponse { status = 1, message = "User added successfully", data = user });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(long id, [FromBody] MobUser updated)
    {
        var user = users.FirstOrDefault(u => u.MobUserId == id);
        if (user == null)
            return NotFound(new ApiResponse { status = 0, message = "User not found" });

        user.FirstName = updated.FirstName;
        user.LastName = updated.LastName;
        user.Email = updated.Email;
        user.PrimMobNo = updated.PrimMobNo;
        user.SeconMobNo = updated.SeconMobNo;
        user.IsActive = updated.IsActive;
        user.IsLocked = updated.IsLocked;

        return Ok(new ApiResponse { status = 1, message = "User updated successfully", data = user });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(long id)
    {
        var user = users.FirstOrDefault(u => u.MobUserId == id);
        if (user == null)
            return NotFound(new ApiResponse { status = 0, message = "User not found" });

        users.Remove(user);
        return Ok(new ApiResponse { status = 1, message = "User deleted successfully" });
    }
}
