using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Helpers.Base;

public static class IdentityHelpers
{
    // extension function
    public static Guid GetUserId(this ClaimsPrincipal user)  // extension method is with: this, 
    {                                              //thanks to this, can use current method better somewhere else
        return Guid.Parse(
            user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
    
    public static string GenerateJwt(IEnumerable<Claim> claims, string key, string issuer, string audience, int expiresInSeconds)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddSeconds(expiresInSeconds);
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}