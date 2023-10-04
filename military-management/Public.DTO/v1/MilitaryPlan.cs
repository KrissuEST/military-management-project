namespace Public.DTO.v1;

public class MilitaryPlan
{
    public Guid Id { get; set; }
    
    public string PlanName { get; set; } = default!;

    public string PlanLocation { get; set; } = default!;

    public string PlanDescription { get; set; } = default!;
    
    public int PersonCount { get; set; } = -1;
}