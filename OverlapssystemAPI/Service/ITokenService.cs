using OverlapssystemDomain;
using OverlapssystemDomain.Entities;

namespace OverlapssystemAPI.Service
{
    public interface ITokenService
    {
        string GenerateToken(UserModel user, IList<string> roles);
    }
}
