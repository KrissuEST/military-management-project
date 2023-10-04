namespace Public.DTO.v1.Identity;

// generating this thing
public class JWTResponse
{
    public string JWT { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;

}