using OverlapssystemDomain.Enums;
using Microsoft.AspNetCore.Identity;


namespace OverlapssystemDomain.Entities
{
    public class UserModel : IdentityUser
    {
        public int? DepartmentId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
