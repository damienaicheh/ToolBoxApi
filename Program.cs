var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/request-content", (HttpContext context) => {
    var headers = context.Request.Headers;
    var contentType = context.Request.ContentType;
    var host = context.Request.Host;

    return new { headers, contentType, host };
})
.WithOpenApi();

var firstNames = new[]
{
    "James", "Mary", "Robert", "Patricia", "John", "Jennifer", "Michael", "Linda", "David", "Elizabeth",
    "William", "Barbara", "Richard", "Susan", "Joseph", "Jessica", "Thomas", "Sarah", "Christopher", "Karen"
};

var lastNames = new[]
{
    "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
    "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin"
};

app.MapGet("/fake-users", () =>
{
    var users = Enumerable.Range(1, 5).Select(index =>
        new FakeUser(
            Id: index,
            FirstName: firstNames[Random.Shared.Next(firstNames.Length)],
            LastName: lastNames[Random.Shared.Next(lastNames.Length)],
            Email: $"user{index}@example.com",
            Age: Random.Shared.Next(18, 80),
            IsActive: Random.Shared.Next(0, 2) == 1
        ))
        .ToArray();
    return users;
})
.WithName("GetFakeUsers")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record FakeUser(int Id, string FirstName, string LastName, string Email, int Age, bool IsActive);
