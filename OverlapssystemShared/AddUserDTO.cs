using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Enums;

namespace OverlapssystemShared
{
    public class AddUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public int? DepartmentId { get; set; }
        public UserRole UserRole { get; set; }

    }
}
