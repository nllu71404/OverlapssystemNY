using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemShared
{
    public class AddUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public record UserRequestDTO(string UserName, string Password);

    }
}
