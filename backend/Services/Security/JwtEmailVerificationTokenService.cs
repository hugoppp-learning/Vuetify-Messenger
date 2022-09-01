using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Controllers;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services.Security;

public class JwtEmailVerificationService
{
    private SigningCredentials SigningCredentials => new(SecurityKey, SecurityAlgorithms.HmacSha256);
    private TokenValidationParameters TokenValidationParameters { get; }
    private SecurityKey SecurityKey { get; }

    private readonly TimeSpan _validFor = TimeSpan.FromHours(12);
    private readonly IConfiguration _config;
    private const string EmailClaim = "Email";

    public JwtEmailVerificationService(IConfiguration config)
    {
        _config = config;
        SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:EmailVerificationKey"]));
        TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = _config["Jwt:Issuer"],
            ValidAudience = _config["Jwt:Audience"],
            IssuerSigningKey = SecurityKey,
        };
    }

    public string GenerateEmailVerificationToken(SignupDto signupDto)
    {
        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            new[] { new Claim(EmailClaim, signupDto.Email) },
            expires: DateTime.Now.Add(_validFor),
            signingCredentials: SigningCredentials
        );
        var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenAsString;
    }


    public string? ValidateEmailToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, TokenValidationParameters, out SecurityToken validatedToken);

            var jwtSecurityToken = validatedToken as JwtSecurityToken;
            IEnumerable<Claim> claims = jwtSecurityToken?.Claims.ToArray() ?? throw new Exception();

            var email = claims.First(o => o.Type == EmailClaim).Value;
            return email;
        }
        catch
        {
            return null;
        }
    }
}