using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Controllers;
using backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace backend;

public class ApplicationJwtConfig
{
    public SigningCredentials SigningCredentials => new(SecurityKey, SecurityAlgorithms.HmacSha256);

    public TokenValidationParameters TokenValidationParameters { get; }

    public SecurityKey SecurityKey { get; }


    private readonly IConfiguration _config;

    public ApplicationJwtConfig(IConfiguration config)
    {
        _config = config;
        SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = _config["Jwt:Issuer"],
            ValidAudience = _config["Jwt:Audience"],
            IssuerSigningKey = SecurityKey,
        };
    }

    public string GenerateEmailVerificationToken(SignupDto signupDto)
    {
        var claims = new[]
        {
            new Claim(EmailClaim, signupDto.Email),
            new Claim(UsernameClaim, signupDto.Username),
            new Claim(IsEmailVerificationToken, "true")
        };

        return GenerateToken(claims);
    }


    public (string Username, string Email)? ValidateEmailToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, TokenValidationParameters, out SecurityToken validatedToken);

            var jwtSecurityToken = validatedToken as JwtSecurityToken;
            IEnumerable<Claim> claims = jwtSecurityToken?.Claims.ToArray() ?? throw new Exception();

            string username = claims.First(o => o.Type == UsernameClaim).Value;
            string email = claims.First(o => o.Type == EmailClaim).Value;

            return (Username: username, Email: email);
        }
        catch
        {
            return null;
        }
    }

    public string GenerateSignInToken(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(EmailClaim, user.Email),
            new Claim(UsernameClaim, user.Username),
        };

        return GenerateToken(claims);
    }


    public readonly OpenApiSecurityScheme OpenApiSecurityScheme = new()
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

    private string GenerateToken(Claim[] claims)
    {
        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddSeconds(30),
            signingCredentials: SigningCredentials
        );
        var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenAsString;
    }

    public const string EmailClaim = "Email";
    public const string UsernameClaim = "Name";
    public const string IsEmailVerificationToken = "EmailVerification";
}