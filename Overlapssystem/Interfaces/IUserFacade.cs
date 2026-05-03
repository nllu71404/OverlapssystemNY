using Overlapssystem.ViewModels;

namespace Overlapssystem.Interfaces
{
    public interface IUserFacade
    {
        Task AddUser(UserViewModel vm);
    }
}
