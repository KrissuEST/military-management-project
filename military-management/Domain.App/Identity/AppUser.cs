using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId   // It have string based primary keys
{
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;
    
    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    
    public ICollection<MilitaryPlan>? MilitaryActivities { get; set; }  //? - nullable
    
    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
}