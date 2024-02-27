namespace Backend;

using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public List<Group> Groups { get; set; } = new List<Group>();
}
