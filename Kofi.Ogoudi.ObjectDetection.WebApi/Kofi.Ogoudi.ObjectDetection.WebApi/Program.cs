using Microsoft.AspNetCore.Mvc;

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

app.MapPost("/ObjectDetection", async ([FromForm] IFormFileCollection files) =>
{
    if (files.Count < 1)
        return Results.BadRequest();
    using var sceneSourceStream = files[0].OpenReadStream();
    using var sceneMemoryStream = new MemoryStream();
    sceneSourceStream.CopyTo(sceneMemoryStream);
    var imageSceneData = sceneMemoryStream.ToArray();
    // Your implementation code
    throw new NotImplementedException();
    // La méthode ci-dessous permet de retourner une image depuis un tableau de bytes,
    var imageData = new bytes[];
    var imageData = new byte[]{};
    return Results.File(imageData, "image/jpg");
}).DisableAntiforgery();


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}