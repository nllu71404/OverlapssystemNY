using Overlapssystem.ViewModels;
using OverlapssytemApplication.Common.Result;

namespace Overlapssystem.Interfaces
{
    public interface IUserFacade
    {
        Task<Result<int>> AddUser(UserViewModel vm);
    }
}
