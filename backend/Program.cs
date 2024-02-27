using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend;

/*

1. Skapa användare
   - Endast som `Admin`
2. Logga in som användare
3. Logga ut som användare
4. Skapa klasser och koppla användare
   - Som `Admin` eller `Lärare`
5. Skapa kurser och koppla till klasser
   - Som `Admin` eller `Lärare`
6. Lägga in betyg för klass, studerande och kurs
   - Som `Admin` eller `Lärare`
7. Se betyg som användare
   - Egna betyg om man är inloggad som `Studerande`
   - Alla betyg om man är inloggad som `Admin` eller `Lärare`
8. Meddela andra användare
   - Med två olika system: email & internt

Användartyper:
- Admin
   - Det finns endast ett Admin konto
- Lärare
- Studerande

*/

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(
                "Host=localhost;Database=schoolSystem;Username=postgres;Password=password"
            );
        });

        builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(
                "create_course",
                policy =>
                {
                    policy.RequireAuthenticatedUser().RequireRole("teacher");
                }
            );
        });

        builder
            .Services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddApiEndpoints();

        builder.Services.AddScoped<GroupService>();
        builder.Services.AddScoped<CourseService>();
        builder.Services.AddTransient<IClaimsTransformation, UserClaimsTransformation>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapIdentityApi<User>();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}

public class UserClaimsTransformation : IClaimsTransformation
{
    UserManager<User> userManager;

    public UserClaimsTransformation(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        ClaimsIdentity claims = new ClaimsIdentity();

        var id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (id != null)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    claims.AddClaim(new Claim(ClaimTypes.Role, userRole));
                }
            }
        }

        principal.AddIdentity(claims);
        return await Task.FromResult(principal);
    }
}
