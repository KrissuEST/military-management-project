namespace Public.DTO.v1;

public class PlanPerson
{
    public Guid Id { get; set; }
    
    public string PersonFirstName { get; set; } = default!;
    
    public string PersonLastName { get; set; } = default!;

    public DateTime PersonBirthday { get; set; }

    public long PersonIdNumber { get; set; }

    public string PersonExtraInfo { get; set; } = default!;
    
    // One-to-many relationship
    public Guid MilitaryPlanId { get; set; }
}