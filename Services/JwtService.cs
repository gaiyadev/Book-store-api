using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreAPI.Services;

public class JwtService
{
    private readonly string _secret;
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
        _secret = _configuration.GetSection("Jwt")["SecretKey"]!;
    }

    public string CreateToken(string email, string username, int id, int roleId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration.GetSection("Jwt")["Issuer"],
            audience: _configuration.GetSection("Jwt")["Audience"],
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