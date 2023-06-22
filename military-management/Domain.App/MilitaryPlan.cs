using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

// Doing my own Domain design.
// Kaveril TrainingPlan
public class MilitaryPlan : DomainEntityId
{
    // one to many relationship
    // Military activity have one one owner but may have different military activities.
    [MaxLength(128)]
    public string PlanName { get; set; } = default!;
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}