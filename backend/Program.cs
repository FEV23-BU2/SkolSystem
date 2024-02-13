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

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}
