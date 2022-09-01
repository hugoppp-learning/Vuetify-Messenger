using System.Text.Json.Serialization;
using backend.Services;
using backend.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

var jwtLoginTokenService = new JwtLoginTokenService(builder.Configuration);
builder.Services.AddSingleton(jwtLoginTokenService);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => { options.TokenValidationParameters = jwtLoginTokenService.TokenValidationParameters; });

builder.Services.AddApplicationSwaggerConfig();

if (builder.Environment.IsDevelopment())
    builder.Services.AddSingleton<IEmailSendingService, LoggerEmailService>();
//else todo add real logger email service

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

app.UseAuthorization();

app.MapControllers();

app.Run();
