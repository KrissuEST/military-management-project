namespace Public.DTO.v1;

public class MilitaryPlanWithPersons
{
    public Guid Id { get; set; }
    public string PersonName { get; set; } = default!;

    public ICollection<PlanPerson> PlanPersons { get; set; } = default!;
}