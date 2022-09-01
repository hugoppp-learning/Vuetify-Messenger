using System.Text.Json.Serialization;
using backend;
using backend.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

ApplicationJwtConfig jwtConfig = new ApplicationJwtConfig(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => options.TokenValidationParameters = jwtConfig.TokenValidationParameters);

builder.Services.AddSwaggerGen(setup =>
{
    setup.AddSecurityDefinition(jwtConfig.OpenApiSecurityScheme.Reference.Id, jwtConfig.OpenApiSecurityScheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtConfig.OpenApiSecurityScheme, Array.Empty<string>() }
    });
});
builder.Services.AddSingleton(jwtConfig);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(b => b
        .WithOrigins("http://localhost:8081",
            "http://localhost:8080")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
    app.Services.GetRequiredService<DevelopmentDataService>().SeedData();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.Use(async (context, next) =>
{
    if (context.User.Claims.Any(x => x.Type == ApplicationJwtConfig.IsEmailVerificationToken))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("E-Mail token can't be used to log in");
    }

    await next.Invoke();
});

app.UseAuthorization();

app.MapControllers();

app.Run();

