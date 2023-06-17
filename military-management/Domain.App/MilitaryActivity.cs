using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

// Doing my own Domain design.
// Käveril TrainingPlan
public class MilitaryActivity : DomainEntityId
{
    // one to many relationship
    // Military activity have one one owner but may have different military activities.
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}