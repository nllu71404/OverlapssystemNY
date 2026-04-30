using OverlapssystemDomain.Enums;
using Microsoft.AspNetCore.Identity;


namespace OverlapssystemDomain.Entities
{
    public class UserModel : IdentityUser
    {
        public string FirstName { get; set; } =string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
    }
}
