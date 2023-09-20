using System.Security.Claims;

namespace Helpers.Base;

public static class IdentityHelpers
{
    // extension function
    public static Guid GetUserId(this ClaimsPrincipal user)  // extension method is with: this, 
    {                                              //thanks to this, can use current method better somewhere else
        return Guid.Parse(
            user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}