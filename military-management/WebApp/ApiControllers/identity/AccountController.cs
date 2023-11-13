using System.Net.Mime;
using Asp.Versioning;
using Domain.App.Identity;
using Helpers.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.v1;
using Public.DTO.v1.Identity;

namespace WebApp.ApiControllers.identity;

[ApiController]  // <- one parameter here, adds support how to do versioning
[ApiVersion("1.0")]   // versioning
// [ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]   // api-s want to controll routing locally
public class AccountController : ControllerBase   // <- it gives me basics of controller
{
    //readonly - we dont wanna change those, do anything with them
    private readonly SignInManager<AppUser> _signInManager;  
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;  // system level configuration support
    
    // our first action - register
    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }
    
    /// <summary>
    /// Register new user to the system
    /// </summary>
    /// <param name="registrationData">user info</param>
    /// <param name="expiresInSeconds">optional, override default value</param>
    /// <returns>JWTResponse with jwt and refresh token</returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JWTResponse>> Register(Register registrationData)
    {
        // is user already registered
        
        // register user
        // we set up the data what we know
        var refreshToken = new AppRefreshToken();
        var appUser = new AppUser()
        {
            Email = registrationData.Email,
            UserName = registrationData.Email,
            FirstName = registrationData.Firstname,
            LastName = registrationData.Lastname,
            AppRefreshTokens = new List<AppRefreshToken>() {refreshToken}
        };
        refreshToken.AppUser = appUser;
        
        var result = await _userManager.CreateAsync(appUser, registrationData.Password);
        
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        
        // generate JWT
        var claimsPrinicipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        
        var jwt = IdentityHelpers.GenerateJwt(
            claimsPrinicipal.Claims,
            _configuration.GetValue<string>("JWT:Key"),
            _configuration.GetValue<string>("JWT:Issuer"),
            _configuration.GetValue<string>("JWT:Audience"),
            expiresInSeconds: 60
        );

        // generate refresh token

        // return result
        return Ok();
    }
}