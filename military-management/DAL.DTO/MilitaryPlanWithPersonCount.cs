namespace DAL.DTO;

public class MilitaryPlanWithPersonCount
{
    public Guid Id { get; set; }
    public string PlanName { get; set; } = default!;
    public int PersonCount { get; set; }
}