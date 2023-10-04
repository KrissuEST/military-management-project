using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

// Here is the added person to Military plan.
public class PlanPerson : DomainEntityId
{
    public string PersonFirstName { get; set; } = default!;
    
    public string PersonLastName { get; set; } = default!;

    public DateTime PersonBirthday { get; set; }

    public long PersonIdNumber { get; set; }

    public string PersonExtraInfo { get; set; } = default!;
    
    // One-to-many relationship
    public Guid MilitaryPlanId { get; set; }

    public MilitaryPlan? MilitaryPlan { get; set; }
}