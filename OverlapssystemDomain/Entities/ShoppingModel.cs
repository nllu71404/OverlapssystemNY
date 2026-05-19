using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Enums;

namespace OverlapssystemDomain.Entities
{
    public class ShoppingModel
    {
        public int ShoppingID { get; set; }
        public int ResidentID { get; set; }
        public Day Day { get; set; }
        public TimeSpan? Time { get; set; } 
        public string PaymentMethod { get; set; } = "";
    }
}
