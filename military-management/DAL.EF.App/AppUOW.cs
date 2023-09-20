using DAL.Contracts.App;
using DAL.EF.App.Repositories;
using DAL.EF.Base;

namespace DAL.EF.App;

// Actual implementation for it
public class AppUOW : EFBaseUOW<ApplicationDbContext>, IAppUOW   //<Type is here>
{
    // Constructor
    public AppUOW(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    private IMilitaryPlanRepository? _militaryPlanRepository;

    public IMilitaryPlanRepository MilitaryPlanRepository =>
        _militaryPlanRepository ??= new MilitaryPlanRepository(UowDbContext);
}