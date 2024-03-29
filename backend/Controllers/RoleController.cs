namespace Backend;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/role")]
public class RoleController : ControllerBase
{
    UserManager<User> userManager;
    RoleManager<IdentityRole> roleManager;

    public RoleController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    [HttpPost("create")]
    public async Task<string> CreateRole([FromQuery] string name)
    {
        await roleManager.CreateAsync(new IdentityRole(name));
        return "Created role " + name;
    }

    [HttpPost("add")]
    public async Task<string> AddUserToRole([FromQuery] string role, [FromQuery] string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return "Failed to find user";
        }

        await userManager.AddToRoleAsync(user, role);
        return "Added role " + role + " to user " + user.UserName;
    }
}
