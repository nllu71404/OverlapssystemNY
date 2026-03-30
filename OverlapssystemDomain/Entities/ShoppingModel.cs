using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemDomain.Entities
{
    public class ShoppingModel
    {
        public int? Id { get; set; }
        public int ResidentID { get; set; }
        public string Day { get; set; } = "";
        public DateTime DateTime { get; set; }
        public string PaymentMethod { get; set; } = "";
    }
}
