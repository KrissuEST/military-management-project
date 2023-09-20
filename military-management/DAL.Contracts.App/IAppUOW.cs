using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IAppUOW : IBaseUOW
{
    // List your repositories here
    IMilitaryPlanRepository MilitaryPlanRepository { get; }
}