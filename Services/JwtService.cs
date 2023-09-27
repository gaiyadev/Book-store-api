using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreAPI.Services;

public sealed class JwtService
{
    private readonly string _jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new InvalidOperationException();
    private readonly string _jwtIssuer = Environment.GetEnvironmentVariable("JWT_SECRET_ISSUER") ?? throw new InvalidOperationException();
    private readonly string _jwtAudience = Environment.GetEnvironmentVariable("JWT_SECRET_AUDIENCE") ?? throw new InvalidOperationException();

    public string CreateToken(string email, string username, int id, int roleId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,
            audience: _jwtAudience,
            claims: new[] {
                new Claim("email", email),
                new Claim("username", username),
                new Claim("role", roleId.ToString()),
                new Claim("id", id.ToString()),
            },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    } 
}