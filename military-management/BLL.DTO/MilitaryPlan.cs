﻿using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;

namespace BLL.DTO;

public class MilitaryPlan : DomainEntityId
{
    [MaxLength(150)]
    public string PlanName { get; set; } = default!;
   
    // One-to-many relationship
    public Guid AppUserId { get; set; }

    public AppUser? AppUser { get; set; }
    
    // Military plan have list of persons.
    public ICollection<PlanPerson>? PlanPersons { get; set; }
}