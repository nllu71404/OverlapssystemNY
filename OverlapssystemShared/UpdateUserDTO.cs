using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemShared
{
    public class UpdateUserDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }
        public string FirstName { get; set; } = string.Empty;  
        public string LastName { get; set; }

    }
}
