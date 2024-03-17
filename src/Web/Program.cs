using Infrastructure;
using Infrastructure.Data;
using Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Swagger
    app.UseSwagger();
    app.UseSwaggerUI();

    // Seed database for development
    await app.InitialiseDatabaseAsync();
}

// Cors enabled
app.UseCors();

// IDK
app.UseAuthorization();

// IDK
app.UseExceptionHandler(options => { });

// Map controllers folder to url path
app.MapControllers();

// Run app
app.Run();