using BLL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAppBLL : IBaseBLL
{
    IMilitaryPlanService MilitaryPlanService { get; }
}