using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using OverlapssystemDomain.Enums;

namespace OverlapssystemDomain.Entities
{
    public class UserModel
    {
        [Key] public int UserID { get; set; }
        public int? DepartmentId { get; set; }
        public string UserName { get; set; } = string.Empty; //må aldrig være null
        public string UserPassword { get; set; } = string.Empty; //må aldrig være null
        public UserRole UserRole { get; set; }
    }
}
