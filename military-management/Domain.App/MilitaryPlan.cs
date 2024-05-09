using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

// Doing my own Domain design.
// A. Kaveril TrainingPlan
public class MilitaryPlan : DomainEntityId
{
    // Military activity have one one owner but may have different military activities.
    [MaxLength(128)]
    public string PlanName { get; set; } = default!;
    
    public DateTime PlanDateTime { get; set; } = default!;  // New

    [MaxLength(150)]
    public string PlanLocation { get; set; } = default!;  // New
    
    [MaxLength(400)]
    public string PlanDescription { get; set; } = default!;  // New
   
    // One-to-many relationship
    public Guid AppUserId { get; set; }

    public AppUser? AppUser { get; set; }
    
    // Military plan have list of persons.
    public ICollection<PlanPerson>? PlanPersons { get; set; }
}