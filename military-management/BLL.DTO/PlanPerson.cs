﻿using Domain.Base;

namespace BLL.DTO;

public class PlanPerson : DomainEntityId
{
    public string PersonFirstName { get; set; } = default!;
    
    public string PersonLastName { get; set; } = default!;

    public bool ActiveSoldierOrOfficial { get; set; } = default!; // new

    public long PersonIdNumber { get; set; }

    public string PersonExtraInfo { get; set; } = default!;
    
    // One-to-many relationship
    public Guid MilitaryPlanId { get; set; }

    public MilitaryPlan? MilitaryPlan { get; set; }
}