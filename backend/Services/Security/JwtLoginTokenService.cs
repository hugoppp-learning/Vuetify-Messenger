using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Model;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services.Security;

public class JwtLoginTokenService
{
    private SigningCredentials SigningCredentials => new(SecurityKey, SecurityAlgorithms.HmacSha256);
    public TokenValidationParameters TokenValidationParameters { get; }
    private SecurityKey SecurityKey { get; }

    private readonly IConfiguration _config;
    private readonly TimeSpan _validFor = TimeSpan.FromDays(365);
    public const string EmailClaim = "Email";

    public JwtLoginTokenService(IConfiguration config)
    {
        _config = config;
        SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:AuthKey"]));
        TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = _config["Jwt:Issuer"],
            ValidAudience = _config["Jwt:Audience"],
            IssuerSigningKey = SecurityKey,
        };
    }

    public string GenerateSignInToken(ApplicationUser user)
    {
        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            new[]
            {
                new Claim(EmailClaim, user.Email)
            },
            expires: DateTime.Now.Add(_validFor),
            signingCredentials: SigningCredentials
        );
        var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenAsString;
    }
}

public static class Ext
{
    public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(JwtLoginTokenService.EmailClaim);
    }
}