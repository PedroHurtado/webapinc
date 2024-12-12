
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using webapi.core.IFeaturModule;
using Microsoft.AspNetCore.Authorization;

namespace webapi.Features.login;

public class Login : IFeatureModule
{
    public readonly record struct Request(string User, string Password);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("login", (IConfiguration configuration, [FromBody] Request request) =>
        {
            if (request.User == "pedro" && request.Password == "12345")
            {
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                var secret = configuration["Jwt:Key"];
                if (secret == null)
                {
                    return Results.Unauthorized();
                }
                var key = Encoding.ASCII.GetBytes(secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                    [
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, request.User),
                        new Claim(JwtRegisteredClaimNames.Email, request.User),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    ]),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Results.Ok(stringToken);

            }
            return Results.Unauthorized();
        }).AllowAnonymous();
    }
}