using System.Security.Principal;
using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId   // It have string based primary keys
{
    public ICollection<MilitaryActivity>? MilitaryActivities { get; set; }  //? - nullable
}