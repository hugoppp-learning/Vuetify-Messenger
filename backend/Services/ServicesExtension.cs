using backend.Repository;
using backend.Repository.Database;
using backend.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace backend.Services;

public static class ServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
                .AddSingleton<AuthService>()
                .AddSingleton<DevelopmentDataService>()
                .AddSingleton<JwtEmailVerificationService>()
                .AddAutoMapper(c => c.AddProfile<DbMapperProfile>())
                //repos
                .AddSingleton<CosmosDbService>()
                .AddSingleton<UserRepo>()
                .AddSingleton<PostRepo>()
            ;
    }

    public static IServiceCollection AddApplicationSwaggerConfig(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSwaggerGen(setup =>
        {
            var openApiSecurityScheme = new OpenApiSecurityScheme()
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            setup.AddSecurityDefinition(openApiSecurityScheme.Reference.Id, openApiSecurityScheme);
            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { openApiSecurityScheme, Array.Empty<string>() }
            });
        });
    }
}