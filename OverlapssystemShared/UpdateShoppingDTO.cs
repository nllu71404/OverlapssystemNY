using OverlapssystemDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemShared
{
    public class UpdateShoppingDTO
    {
        public int ShoppingID { get; set; }
        public int ResidentID { get; set; }
        public Day Day { get; set; }
        public TimeSpan? Time { get; set; }
        public string PaymentMethod { get; set; } = "";
    }
}
